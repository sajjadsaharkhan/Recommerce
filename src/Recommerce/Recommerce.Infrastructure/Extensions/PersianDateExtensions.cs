using System.Globalization;
using JetBrains.Annotations;
using Recommerce.Infrastructure.Enums;

namespace Project.Infrastructure.Extensions;

[PublicAPI]
public static class PersianDateExtensions
{
    private const int Second = 1;
    private const int Minute = 60 * Second;
    private const int Hour = 60 * Minute;
    private const int Day = 24 * Hour;
    private const int Month = 30 * Day;

    /// <summary>
    /// Get persian name of month
    /// </summary>
    /// <param name="month"></param>
    /// <returns></returns>
    public static string ToPersianMonthName(this int month)
    {
        return month switch
        {
            1 => "فروردین",
            2 => "اردیبهشت",
            3 => "خرداد",
            4 => "تیر",
            5 => "مرداد",
            6 => "شهریور",
            7 => "مهر",
            8 => "آبان",
            9 => "آذر",
            10 => "دی",
            11 => "بهمن",
            12 => "اسفند",
            _ => throw new ArgumentOutOfRangeException(nameof(month))
        };
    }

    /// <summary>
    /// Get persian name of day of week
    /// </summary>
    /// <param name="dayOfWeek"></param>
    /// <returns></returns>
    public static string ToPersianDayOfWeekName(this DayOfWeek dayOfWeek)
    {
        return dayOfWeek switch
        {
            DayOfWeek.Saturday => "شنبه",
            DayOfWeek.Sunday => "یکشنبه",
            DayOfWeek.Monday => "دوشنبه",
            DayOfWeek.Tuesday => "سه شنبه",
            DayOfWeek.Wednesday => "چهارشنبه",
            DayOfWeek.Thursday => "پنجشنبه",
            DayOfWeek.Friday => "جمعه",
            _ => throw new ArgumentOutOfRangeException(nameof(dayOfWeek))
        };
    }

    /// <summary>
    /// Convert date time to Shamsi short date as enum pattern
    /// </summary>
    /// <param name="date"></param>
    /// <param name="pattern"></param>
    /// <returns></returns>
    public static string ToPersianDate(this DateTime date, PersianDatePatterns pattern)
    {
        var pc = new PersianCalendar();
        return pattern switch
        {
            PersianDatePatterns.Standard =>
                $"{pc.GetYear(date)}/{pc.GetMonth(date)}/{pc.GetDayOfMonth(date)}",

            PersianDatePatterns.Pattern1 =>
                $"{pc.GetHour(date)}:{pc.GetMinute(date)} | {pc.GetDayOfMonth(date)}-{pc.GetMonth(date).ToPersianMonthName()}",

            PersianDatePatterns.Pattern2 =>
                $"{pc.GetDayOfMonth(date)}-{pc.GetMonth(date).ToPersianMonthName()} {pc.GetHour(date)}:{pc.GetMinute(date)}",

            PersianDatePatterns.Pattern3 =>
                $"{pc.GetYear(date)} {pc.GetMonth(date).ToPersianMonthName()} {pc.GetDayOfMonth(date)} {pc.GetHour(date)}:{pc.GetMinute(date)}",

            PersianDatePatterns.Pattern4 =>
                $"{pc.GetYear(date)}/{pc.GetMonth(date)}/{pc.GetDayOfMonth(date)} {date.Hour}:{date.Minute}",

            PersianDatePatterns.Pattern5 =>
                $"{pc.GetDayOfWeek(date).ToPersianDayOfWeekName()} {pc.GetDayOfMonth(date)} {pc.GetMonth(date).ToPersianMonthName()} {pc.GetYear(date)}",

            PersianDatePatterns.Pattern6 =>
                $"{pc.GetDayOfMonth(date)} {pc.GetMonth(date).ToPersianMonthName()} {pc.GetYear(date)}",

            PersianDatePatterns.Pattern7 =>
                $"{pc.GetDayOfWeek(date).ToPersianDayOfWeekName()} {pc.GetMonth(date)}/{pc.GetDayOfMonth(date)}",

            PersianDatePatterns.Pattern8 =>
                $"{pc.GetDayOfWeek(date).ToPersianDayOfWeekName()} {pc.GetYear(date)}/{pc.GetMonth(date)}/{pc.GetDayOfMonth(date)}",

            PersianDatePatterns.Pattern9 =>
                $"{pc.GetYear(date)}-{pc.GetMonth(date)}-{pc.GetDayOfMonth(date)} {pc.GetHour(date)}:{pc.GetMinute(date)}:{pc.GetSecond(date)}",

            PersianDatePatterns.Pattern10 =>
                $"{pc.GetDayOfMonth(date)}-{pc.GetMonth(date).ToPersianMonthName()}",

            _ => throw new ArgumentOutOfRangeException(nameof(pattern), pattern, null)
        };
    }

    /// <summary>
    /// Convert date time to persian date and send empty string on null datetime
    /// </summary>
    /// <param name="date"></param>
    /// <param name="pattern"></param>
    /// <returns></returns>
    public static string ToPersianDate(this DateTime? date, PersianDatePatterns pattern)
    {
        return date.HasValue ? date.Value.ToPersianDate(pattern) : string.Empty;
    }
}