using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Recommerce.Infrastructure.Extensions;

namespace Project.Infrastructure.Extensions;

[PublicAPI]
public class HttpRequestBuilder
{
    private HttpClient _httpClient;

    private Uri BaseUrl { get; set; }
    private string Url { get; set; }

    private HttpMethod HttpMethod { get; set; }

    private MultipartFormDataContent _multipartFormDataContent;
    private object JsonBody { get; set; }
    private bool AcceptOnlyJson { get; set; }

    private bool _ensureSuccessStatusCode;

    private Dictionary<HttpStatusCode, Func<Task>> _onFailedApiResultFuncDictionary = new();

    private int? TimeOutInSecond { get; set; }

    private readonly IDictionary<string, string> _queryParams;
    private readonly IDictionary<string, string> _headers;

    public HttpRequestBuilder(HttpClient httpClient)
    {
        _httpClient = httpClient;
        HttpMethod = HttpMethod.Get;

        TimeOutInSecond = null;
        AcceptOnlyJson = false;

        _queryParams = new Dictionary<string, string>();
        _headers = new Dictionary<string, string>();
    }

    public HttpRequestBuilder SetBaseUrl(string uri)
    {
        BaseUrl = new Uri(uri);
        return this;
    }

    public HttpRequestBuilder SetUrl(string uri)
    {
        Url = uri;
        return this;
    }

    public HttpRequestBuilder AddOnFailedAction(HttpStatusCode statusCode, Func<Task> func)
    {
        if (_onFailedApiResultFuncDictionary.ContainsKey(statusCode))
            _onFailedApiResultFuncDictionary.Add(statusCode, func);
        else
            _onFailedApiResultFuncDictionary[statusCode] = func;

        return this;
    }

    public HttpRequestBuilder SetRequestTimeOut(int timeOutInSecond)
    {
        TimeOutInSecond = timeOutInSecond;
        return this;
    }

    public HttpRequestBuilder EnsureSuccessStatusCode(bool isActive = true)
    {
        _ensureSuccessStatusCode = isActive;
        return this;
    }

    public HttpRequestBuilder SetMethod(HttpMethod httpMethod)
    {
        HttpMethod = httpMethod;
        return this;
    }

    public HttpRequestBuilder SetJsonBody(object body)
    {
        JsonBody = body;
        return this;
    }

    public HttpRequestBuilder SetAcceptOnlyJson()
    {
        AcceptOnlyJson = true;
        return this;
    }

    public HttpRequestBuilder AddQueryParam(string key, string value)
    {
        _queryParams.Add(key, value);
        return this;
    }

    public HttpRequestBuilder AddQueryParam(string key, object value)
    {
        AddQueryParam(key, value.ToString());
        return this;
    }

    public HttpRequestBuilder AddHeader(string key, object value)
    {
        _headers.Add(key, value.ToString());
        return this;
    }

    public HttpRequestBuilder AddHeader(KeyValuePair<string, string> header)
    {
        _headers.Add(header.Key, header.Value);
        return this;
    }

    public async Task<HttpRequestMessage> BuildHttpRequestMessage(CancellationToken cancellationToken)
    {
        var uriBuilder = _httpClient.BaseAddress is not null
            ? new UriBuilder(new Uri(_httpClient.BaseAddress, Url))
            : new UriBuilder(new Uri(BaseUrl, Url));

        if (_queryParams.Any())
        {
            using var content = new FormUrlEncodedContent(_queryParams
                .Select(c => new KeyValuePair<string, string>(c.Key, c.Value)));
            uriBuilder.Query = await content.ReadAsStringAsync(cancellationToken);
        }

        var httpRequestMessage = new HttpRequestMessage
        {
            Method = HttpMethod,
            RequestUri = uriBuilder.Uri
        };

        if (AcceptOnlyJson)
        {
            httpRequestMessage.Headers.Accept.Clear();
            httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        foreach (var (key, value) in _headers)
            httpRequestMessage.Headers.Add(key, value);

        if (JsonBody is not null)
            httpRequestMessage.Content = JsonContent.Create(JsonBody, options: new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

        if (_multipartFormDataContent is not null)
            httpRequestMessage.Content = _multipartFormDataContent;

        return httpRequestMessage;
    }

    public async Task<TResult> FromJsonAsync<TResult>(CancellationToken cancellationToken)
    {
        var stringResponse = await FromStringAsync(cancellationToken);
        return JsonConvert.DeserializeObject<TResult>(stringResponse);
    }

    public async Task<string> FromStringAsync(CancellationToken cancellationToken)
    {
        var httpResponseMessage = TimeOutInSecond.HasValue
            ? await _sendWithTimeoutAsync(cancellationToken)
            : await _sendAsync(cancellationToken);

        var stringResponse = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);

        return stringResponse;
    }


    public async Task<(HttpStatusCode StatusCode, string Result)> FromStringWithStatusCodeAsync(
        CancellationToken cancellationToken)
    {
        var httpResponseMessage = TimeOutInSecond.HasValue
            ? await _sendWithTimeoutAsync(cancellationToken)
            : await _sendAsync(cancellationToken);

        var stringResponse = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);

        return (httpResponseMessage.StatusCode, stringResponse);
    }

    public HttpRequestBuilder SetMultipartFormData(MultipartFormDataContent multipartFormDataContent)
    {
        _multipartFormDataContent = multipartFormDataContent;
        return this;
    }

    private async Task<HttpResponseMessage> _sendWithTimeoutAsync(CancellationToken cancellationToken)
    {
        if (!TimeOutInSecond.HasValue)
            throw new NullReferenceException(nameof(TimeOutInSecond));

        async Task<HttpResponseMessage> TimeoutFuncTask()
        {
            await Task.Delay(TimeSpan.FromSeconds(TimeOutInSecond!.Value), cancellationToken);
            throw new Exception(
                $"request timed out in {TimeOutInSecond!.Value} second. [RequestUrl]=({BaseUrl + Url}) , [RequestBody]=({JsonBody.Serialize()})");
        }

        var timeoutTask = Task.Run((Func<Task<HttpResponseMessage>>)TimeoutFuncTask, cancellationToken);

        var httpRequestTask = _httpClient.SendAsync(await BuildHttpRequestMessage(cancellationToken),
            cancellationToken);
        var completedTask = await Task.WhenAny(timeoutTask, httpRequestTask);
        var httpResponse = await completedTask;

        if (!httpResponse.IsSuccessStatusCode &&
            _onFailedApiResultFuncDictionary.ContainsKey(httpResponse.StatusCode))
            await _onFailedApiResultFuncDictionary[httpResponse.StatusCode]();

        return httpResponse;
    }

    private async Task<HttpResponseMessage> _sendAsync(CancellationToken cancellationToken)
    {
        var httpResponseMessage =
            await _httpClient.SendAsync(await BuildHttpRequestMessage(cancellationToken), cancellationToken);

        if (_ensureSuccessStatusCode)
            httpResponseMessage.EnsureSuccessStatusCode();

        if (!httpResponseMessage.IsSuccessStatusCode &&
            _onFailedApiResultFuncDictionary.ContainsKey(httpResponseMessage.StatusCode))
            await _onFailedApiResultFuncDictionary[httpResponseMessage.StatusCode]();

        return httpResponseMessage;
    }
}