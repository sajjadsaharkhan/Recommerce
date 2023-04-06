using JetBrains.Annotations;

namespace Recommerce.Infrastructure.Exceptions;

/// <summary>
/// Occurs when the server cannot find the resource the client is looking for
/// </summary>
[PublicAPI]
public class NotFoundException : BaseException
{
    /// <summary>
    /// Creates an instance of this exception with the resource id(s)
    /// </summary>
    /// <param name="source">The searching source name</param>
    /// <param name="ids">The resource's id(s) that failed to access its records</param>
    public NotFoundException(string source, params object[] ids) 
        : base($"Cannot found data in '{source}' with this ids: ({string.Join(", ", ids)})")
    {
    }
}