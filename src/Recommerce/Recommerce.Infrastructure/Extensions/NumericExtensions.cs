using JetBrains.Annotations;

namespace Project.Infrastructure.Extensions;

[PublicAPI]
public static class NumericExtensions
{
    /// <summary>
    /// check input number zero or null
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public static bool IsNullOrDefault(this int? number)
    {
        return number is 0 or null;
    }
}