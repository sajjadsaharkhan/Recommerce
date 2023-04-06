using JetBrains.Annotations;

namespace Recommerce.Infrastructure.Enums;

[PublicAPI]
public enum PersianDatePatterns : byte
{
    /// <summary>
    /// [Year]/[Month]/[Day]
    /// </summary>
    Standard = 0,

    /// <summary>
    /// [Hour]:[Minute] | [Day]-[MonthName] 
    /// </summary>
    Pattern1 = 1,

    /// <summary>
    /// [Day]-[MonthName] [Hour]:[Minute]
    /// </summary>
    Pattern2 = 2,

    /// <summary>
    /// [Year] [MonthName] [Day] [Hour]:[Minute]
    /// </summary>
    Pattern3 = 3,

    /// <summary>
    /// [Year]/[Month]/[Day] [Hour]:[Minute]
    /// </summary>
    Pattern4 = 4,

    /// <summary>
    /// [WeekDay] [Day] [MonthName] [Year]
    /// </summary>
    Pattern5 = 5,

    /// <summary>
    /// [Day] [MonthName] [Year]
    /// </summary>
    Pattern6 = 6,

    /// <summary>
    /// [WeekDay] [Day]/[Month] 
    /// </summary>
    Pattern7 = 7,

    /// <summary>
    /// [WeekDay] [Day]/[Month]/[Year]
    /// </summary>
    Pattern8 = 8,

    /// <summary>
    /// [Year]-[Month]-[Day] [Hour]:[Minute]:[Sec]
    /// </summary>
    Pattern9 = 9,
    
    /// <summary>
    /// [Day] [MonthName]
    /// </summary>
    Pattern10 = 10
}