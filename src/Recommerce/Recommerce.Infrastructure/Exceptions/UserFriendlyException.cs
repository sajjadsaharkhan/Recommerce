using System.Net;
using JetBrains.Annotations;

namespace Recommerce.Infrastructure.Exceptions;

/// <summary>
/// Occurs when the client should do something and then try again
/// </summary>
[PublicAPI]
public class UserFriendlyException : BaseException
{
    /// <summary>
    /// The code of error. Use when the front-end implemented specific logic for this specific error 
    /// </summary>
    public HttpStatusCode? ErrorCode { get; }

    /// <summary>
    /// Occurs when the client should do something and then try again
    /// </summary>
    /// <param name="message">The message clint should see</param>
    public UserFriendlyException(string message) : base(message)
    {
    }
        
    /// <summary>
    /// Occurs when the client should do something and then try again
    /// </summary>
    /// <param name="message">The message that the client should see</param>
    /// <param name="errorCode">Code of a specific error</param>
    public UserFriendlyException(string message, HttpStatusCode errorCode) : base(message)
    {
        ErrorCode = errorCode;
    }

}