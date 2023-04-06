using JetBrains.Annotations;

namespace Recommerce.Infrastructure.Exceptions;

/// <summary>
/// Occurs when the server cannot find the resource the client is looking for
/// </summary>
/// <typeparam name="T">Type of resource entity</typeparam>
[PublicAPI]
public class EntityNotFoundException<T> : BaseException
{
    /// <summary>
    /// Creates an instance of this exception with the resource id(s)
    /// </summary>
    /// <param name="ids">The resource's id(s) that failed to access its records</param>
    public EntityNotFoundException(params object[] ids)
        : base($"Cannot found data in '{typeof(T)}' with this ids: ({string.Join(", ", ids)})")
    {
    }
}