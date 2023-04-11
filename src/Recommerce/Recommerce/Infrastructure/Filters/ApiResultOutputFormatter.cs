using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Recommerce.Infrastructure.Filters;

internal class ApiResultOutputFormatter : IOutputFormatter
{
    public bool CanWriteResult(OutputFormatterCanWriteContext context) => true;

    public Task WriteAsync(OutputFormatterWriteContext context)
    {
        var result = context.ObjectType == typeof(ApiResult)
            ? context.Object
            : ApiResult.Succeed(context.Object!);
        
        return context.HttpContext.Response.WriteAsJsonAsync(result);
    }
}