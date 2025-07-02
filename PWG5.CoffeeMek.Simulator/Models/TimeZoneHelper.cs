namespace PWG5.CoffeeMek.Simulator.Models;

public static class TimeZoneHelper
{
    public static TimeZoneInfo GetTimeZoneForLocation(LocationEnum location) => location switch
    {
        LocationEnum.Italy => TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time"),
        LocationEnum.Brazil => TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"),
        LocationEnum.Vietnam => TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"),
        _ => TimeZoneInfo.Utc,
    };
}