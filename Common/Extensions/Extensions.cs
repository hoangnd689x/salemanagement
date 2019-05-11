using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Common.Extensions
{
    public static class Extensions
    {
        public static string ToViFormat(this DateTime date)
        {

            return string.Format("{0:dd-MM-yyyy}", date);
        }

        public static int ToMinutes(this int seconds)
        {
            return Convert.ToInt32(seconds / 60);
        }

        public static DateTime GetDateByViFormat(this string date)
        {
            bool tmp;
            return date.GetDateByViFormat(out tmp);

        }


        public static DateTime GetDateByViFormat(this string date, out bool status)
        {
            status = true;
            try
            {

                DateTime dt;
                if (DateTime.TryParseExact(date, "d/M/yyyy", null,
                                       DateTimeStyles.None,
                    out dt))
                {
                    //valid date

                    return dt;
                }
                else
                {
                    status = false;
                    return DateTime.Now;
                }

            }
            catch (Exception ex)
            {
                status = false;
                return DateTime.Now;
            }

        }
    }
}
