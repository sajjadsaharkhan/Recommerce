using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Recommerce.Infrastructure.Extensions;

[PublicAPI]
public static class JsonHelper
{
    public static bool IsValidJson(this string strInput)
    {
        if (string.IsNullOrWhiteSpace(strInput))
            return false;

        strInput = strInput.Trim();
        if ((!strInput.StartsWith("{") || !strInput.EndsWith("}")) &&
            (!strInput.StartsWith("[") || !strInput.EndsWith("]"))) return false;
        try
        {
            JToken.Parse(strInput);
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    public static string Serialize(this object obj)
    {
        if (obj is null)
            return "";
        var settings = _jsonSerializerSettings();
        return JsonConvert.SerializeObject(obj, settings);
    }

    public static TSource Deserialize<TSource>(this string serializedString)
    {
        if (string.IsNullOrWhiteSpace(serializedString))
            return default;

        var settings = _jsonSerializerSettings();
        return JsonConvert.DeserializeObject<TSource>(serializedString.Trim(), settings);
    }
    
    private static JsonSerializerSettings _jsonSerializerSettings()
    {
        var contractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        };
        return new JsonSerializerSettings { ContractResolver = contractResolver };
    }
}