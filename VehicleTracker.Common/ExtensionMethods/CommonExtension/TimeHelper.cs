using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracker.Common
{
    public static class TimeHelper
    {
        public static DateTime ConvertToUserTime(DateTime utcTime, string timeZone)
        {
            var userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
            var today = TimeZoneInfo.ConvertTimeFromUtc(utcTime, userTimeZone);

            return today;

        }
    }
}
