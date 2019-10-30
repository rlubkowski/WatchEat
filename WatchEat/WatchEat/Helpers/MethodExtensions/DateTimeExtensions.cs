using System;

namespace WatchEat.Helpers.MethodExtensions
{
    public static class DateTimeExtensions
    {
        public static TimeSpan ToTimespan(this DateTime dateTime)
        {            
            return new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second);
        }

        public static DateTime AppendCurrentTime(this DateTime dateTime)
        {
            var current = DateTime.Now;
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, current.Hour, current.Minute, current.Second);
        }
    }
}
