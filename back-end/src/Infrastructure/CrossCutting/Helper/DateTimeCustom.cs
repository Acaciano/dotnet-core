using System;

namespace Infrastructure.CrossCutting.Helper
{
    public static class DateTimeCustom
    {
        public static TimeZoneInfo BrazilTimeZoneInfo
        {
            get
            {
                return TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            }
        }

        public static DateTime ToBrazillianDate(this DateTime date)
        {
            return TimeZoneInfo.ConvertTime(date, BrazilTimeZoneInfo);
        }

        public static DateTime ToUtc(this DateTime date)
        {
            return TimeZoneInfo.ConvertTime(date, TimeZoneInfo.Utc);
        }

        public static DateTime ToUtcFromUnknownKind(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, DateTimeKind.Local).ToUniversalTime();
        }
    }
}
