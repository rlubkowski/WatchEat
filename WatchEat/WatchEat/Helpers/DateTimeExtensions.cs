using System;

namespace WatchEat.Helpers
{
    public static class DateTimeExtensions
    {
        public static TimeSpan ToTimespan(this DateTime dateTime)
        {            
            return new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second);
        }
    }
}
