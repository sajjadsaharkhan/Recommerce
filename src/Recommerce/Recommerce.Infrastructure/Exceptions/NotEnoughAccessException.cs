using JetBrains.Annotations;

namespace Recommerce.Infrastructure.Exceptions;

/// <summary>
/// Occurs when the user is logged in, but haven't enough access
/// </summary>
[PublicAPI]
public class NotEnoughAccessException : BaseException
{
}