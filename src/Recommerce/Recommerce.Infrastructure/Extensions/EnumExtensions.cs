using System.ComponentModel.DataAnnotations;
using System.Reflection;
using JetBrains.Annotations;
using Recommerce.Infrastructure.Enums;

namespace Recommerce.Infrastructure.Extensions;

[PublicAPI]
public static class EnumExtensions
{
    /// <summary>
    /// Get enum values
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="input"></param>
    /// <returns></returns>
    public static IEnumerable<T> GetEnumValues<T>(this T input) where T : struct
    {
        if (!typeof(T).IsEnum)
            throw new NotSupportedException();

        return Enum.GetValues(input.GetType()).Cast<T>();
    }

    /// <summary>
    /// Get enum flag
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="input"></param>
    /// <returns></returns>
    public static IEnumerable<T> GetEnumFlags<T>(this T input) where T : struct
    {
        if (!typeof(T).IsEnum)
            throw new NotSupportedException();

        foreach (var value in Enum.GetValues(input.GetType()))
            // ReSharper disable once PossibleNullReferenceException
            if ((input as Enum)!.HasFlag(value as Enum ?? throw new InvalidOperationException()))
                yield return (T) value;
    }

    /// <summary>
    /// Get display fields (default = name) of enum
    /// </summary>
    /// <param name="value"></param>
    /// <param name="property"></param>
    /// <returns></returns>
    public static string ToDisplay(this Enum value, EnumDisplayProperty property = EnumDisplayProperty.Name)
    {
        if (value == null)
            throw new ArgumentNullException(nameof(value));

        var attribute = ((value.GetType().GetField(value.ToString())) ?? throw new InvalidOperationException())
            .GetCustomAttributes<DisplayAttribute>(false).FirstOrDefault();

        if (attribute == null)
            return value.ToString();

        var propValue = attribute.GetType().GetProperty(property.ToString())?.GetValue(attribute, null);

        return propValue != null 
            ? propValue.ToString() ?? string.Empty 
            : string.Empty;
    }

    /// <summary>
    /// Get display fields (default = name) of enum
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToNameDisplay(this Enum value)
    {
        return ToDisplay(value);
    }
    
    /// <summary>
    /// To dictionary
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Dictionary<int, string> ToDictionary(this Enum value)
    {
        return Enum.GetValues(value.GetType()).Cast<Enum>()
            .ToDictionary(Convert.ToInt32, ToNameDisplay);
    }

    /// <summary>
    /// Converts strings to target (T) enum
    /// converts string using flag names or it's [Display] attributes throws exception otherwise
    /// </summary>
    /// <param name="input"></param>
    /// <param name="property"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="Exception"></exception>
    public static T ToEnumValue<T>(this string input, EnumDisplayProperty property = EnumDisplayProperty.Name)
        where T : struct, Enum
    {
        if (!typeof(T).IsEnum)
            throw new NotSupportedException();

        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentNullException(nameof(input));

        if (Enum.TryParse<T>(input, true, out var enumKey))
            return enumKey;

        foreach (T key in Enum.GetValues(typeof(T)))
        {
            var attribute = typeof(T)
                .GetField(key.ToString())
                ?.GetCustomAttribute<DisplayAttribute>(false);

            var propertyValue = attribute
                ?.GetType()
                .GetProperty(property.ToString())
                ?.GetValue(attribute, null)
                ?.ToString();

            if (!string.IsNullOrWhiteSpace(propertyValue) && propertyValue == input)
                return key;
        }

        throw new Exception($"Supplied resource ({input}) could not be found on enum ({typeof(T).Name})");
    }
}