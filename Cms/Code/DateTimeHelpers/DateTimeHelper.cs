using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Code.DateTimeHelpers
{
    public class DateTimeHelper
    {
        static string[] formats = new string[]
        {
            "MM/dd/yyyy HH:mm:ss tt",
            "dd/MM/yyyy HH:mm:ss tt",
            "MM/d/yyyy HH:mm:ss tt",
            "d/M/yyyy HH:mm:ss tt",
            "M/d/yyyy HH:mm:ss tt",
            "M/dd/yyyy HH:mm:ss tt",
            "MM/dd/yyyy H:mm:ss tt",
            "dd/MM/yyyy H:mm:ss tt",
            "MM/d/yyyy H:mm:ss tt",
            "M/dd/yyyy H:mm:ss tt",
            "d/M/yyyy H:mm:ss tt",
            "M/d/yyyy H:mm:ss tt",
            "MM/dd/yyyy HH:mm tt",
            "dd/MM/yyyy HH:mm tt",
            "MM/d/yyyy HH:mm tt",
            "d/M/yyyy HH:mm tt",
            "M/d/yyyy HH:mm tt",
            "MM/dd/yyyy HH:mm",
            "dd/MM/yyyy HH:mm",
            "MM/d/yyyy HH:mm",
            "d/M/yyyy HH:mm",
            "M/d/yyyy HH:mm",
            "MM/dd/yyyy h:mm:ss tt",
            "dd/MM/yyyy h:mm:ss tt",
            "MM/d/yyyy h:mm:ss tt",
            "d/M/yyyy h:mm:ss tt",
            "M/d/yyyy h:mm:ss tt",
            "M/dd/yyyy h:mm:ss tt",
            "MM/dd/yyyy h:mm:ss tt",
            "dd/MM/yyyy h:mm:ss tt",
            "MM/d/yyyy h:mm:ss tt",
            "M/dd/yyyy h:mm:ss tt",
            "d/M/yyyy h:mm:ss tt",
            "M/d/yyyy h:mm:ss tt",
            "MM/dd/yyyy h:mm tt",
            "dd/MM/yyyy h:mm tt",
            "MM/d/yyyy h:mm tt",
            "d/M/yyyy h:mm tt",
            "M/d/yyyy h:mm tt",
            "dd-MM-yyyy",
            "yyyy-MM-dd",
            "yyyy/MM/dd",
            "dd/MM/yyyy",
            "dd/mm/yyyy hh:mm:ss",
            "dd/MM/yyyy HH:mm:ss",
            "MM/dd/yyyy HH:mm",
        };
        static string[] formatdate = new string[]
        {
            "yyyy-MM-dd",

        };

        public static DateTime ConvertStringToDate(string input)
        {
            return DateTime.ParseExact(input, formats, CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal);
        }
        public static string SecondsToHMS(double num)
        {
            string val = "";
            double h = Math.Floor(num / 3600);
            double m = Math.Floor(num % 3600 / 60);
            double s = Math.Floor(num % 3600 % 60);
            var hr = format(h);
            var min = format(m);
            var sec = format(s);
            val = hr + ':' + min;// + ':' + sec;
            return val;
        }

        private static string format(double num)
        {
            string val = "";
            if (num > 0)
            {
                if (num >= 10)
                    val = num.ToString();
                else
                    val = "0" + num.ToString();
            }
            else
            {
                val = "00";
            }


            return val;
        }
    }
}
