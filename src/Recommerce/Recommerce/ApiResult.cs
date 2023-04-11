using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace Recommerce;

[PublicAPI]
[JsonSourceGenerationOptions(DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
public class ApiResult
{
    public bool Success { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public object Result { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public HttpStatusCode? ErrorCode { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Error { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Dictionary<string, List<string>> Validations { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Exception { get; set; }

    public static ApiResult Failed(HttpStatusCode code, string message)
    {
        return new ApiResult {ErrorCode = code, Error = message};
    }
    
    public static ApiResult Failed(string message)
    {
        return new ApiResult {Error = message};
    }

    public static ApiResult Failed(Exception exception)
    {
        return new ApiResult {Exception = exception.ToString()};
    }
    
    public static ApiResult Failed(Dictionary<string, List<string>> validationErrors)
    {
        return new ApiResult {Validations = validationErrors};
    }

    public static ApiResult Succeed()
    {
        return new ApiResult {Success = true};
    }
    
    public static ApiResult Succeed(object result)
    {
        return new ApiResult {Success = true, Result = result};
    }

}