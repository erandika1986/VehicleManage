using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
  public static class DateTimeExtension
  {
    public static DateTime ConvertToUserTime(this DateTime utcTime,string timeZoneId)
    {
      TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
      var userTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, timeZoneInfo);

      return userTime;
    }
  }
}
