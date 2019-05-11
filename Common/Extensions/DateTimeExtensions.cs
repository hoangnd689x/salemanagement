using Common.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static long ToUnixTime(this DateTime date)
        {

            var timeSpan = (date - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)timeSpan.TotalSeconds;
        }

        //public static string ToTimeAgo(this DateTime date)
        //{
        //    return StringHelpers.TimeAgo(date);
        //}

        public static string ToViDate(this DateTime date)
        {
            return string.Format("{0} giờ {1} phút ngày {2} tháng {3} năm {4}", date.Hour, date.Minute, date.Day, date.Month, date.Year);
        }


    }
}
