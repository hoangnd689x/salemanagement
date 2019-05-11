using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Common.Helpers
{

    public class DateTimeHelpers
    {
        public static int GetWeekOfYear(DateTime dt)
        {
            return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(dt, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

        }

        public static int GetWeekOfMonth(DateTime dt)
        {
            return (GetWeekOfYear(dt) - GetWeekOfYear(dt.AddDays(1 - dt.Day)) + 1);
        }

        public static int WeekOfDate
        {
            get
            {
                if (DateTime.Now.Day < 8)
                {
                    return 1;
                }
                if (DateTime.Now.Day < 15)
                {
                    return 2;
                }
                return DateTime.Now.Day < 22 ? 3 : 4;
            }
        }


    }

}
