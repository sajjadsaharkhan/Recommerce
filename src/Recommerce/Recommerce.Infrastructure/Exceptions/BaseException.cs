using System.Net;

namespace Recommerce.Infrastructure.Exceptions;

public abstract class BaseException : Exception
{
    /// <summary>Base class for all exceptions thrown by problem details</summary>
    /// <param name="message"></param>
    /// <param name="statusCode"></param>
    /// <param name="code"></param>
    public BaseException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest, int? code = null)
        : base(message)
    {
        StatusCode = statusCode;
        Code = code;
    }

    /// <summary>Base class for all exceptions thrown by problem details</summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    /// <param name="statusCode"></param>
    /// <param name="code"></param>
    public BaseException(string message, Exception innerException,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest, int? code = null) : base(message, innerException)
    {
        StatusCode = statusCode;
        Code = code;
    }

    /// <summary>Base class for all exceptions thrown by problem details</summary>
    /// <param name="statusCode"></param>
    /// <param name="code"></param>
    public BaseException(HttpStatusCode statusCode = HttpStatusCode.BadRequest, int? code = null)
    {
        StatusCode = statusCode;
        Code = code;
    }

    public HttpStatusCode StatusCode { get; }

    public int? Code { get; }
}