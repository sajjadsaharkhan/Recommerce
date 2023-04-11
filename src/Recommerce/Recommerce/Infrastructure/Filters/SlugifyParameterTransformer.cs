using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Routing;

namespace Recommerce.Infrastructure.Filters;

internal class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    public string TransformOutbound(object value)
    {
        return value == null
            ? null
            : Regex.Replace(value.ToString()!, "([a-z])([A-Z])", "$1-$2").ToLower();
    }
}