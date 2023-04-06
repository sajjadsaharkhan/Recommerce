using System.Globalization;
using JetBrains.Annotations;

namespace Project.Infrastructure.Extensions;

[PublicAPI]
public static class DateExtensions
{
    /// <summary>
    /// split "yyyymmdd" date to yyyy/mm/dd
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static string SplitToDate(this string date)
    {
        if (string.IsNullOrEmpty(date))
            return string.Empty;

        if (date.Length != 8)
            return string.Empty;

        if (date.Contains('/') || date.Contains('-'))
            return string.Empty;

        return $"{date[..4]}/" +
               $"{date.Substring(4, 2)}/" +
               $"{date.Substring(6, 2)}";
    }

    public static string ToTaminDateTimeFormat(this DateTime date)
    {
        var persianCalendar = new PersianCalendar();
        return
            $"{persianCalendar.GetYear(date):0000}{persianCalendar.GetMonth(date):00}{persianCalendar.GetDayOfMonth(date):00}";
    }
}