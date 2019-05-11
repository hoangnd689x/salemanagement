using Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Common.Extensions
{
    public static class ModelExtension
    {

        public static string ToFloorText(this double floor)
        {
            return string.Format("{0} tầng", floor);
        }
        public static string ToPasswordHash(this string password)
        {
            return StringHelpers.MD5Hash(password);
        }
        public static string ToDateTime(this DateTime dt)
        {
            return string.Format("{0:hh:mm dd-MM-yyyy}", dt);
        }

        public static List<int> StringToInts(this string str, char c = '|')
        {

            var result = new List<int>();
            foreach (var item in str.Split(c))
            {
                if (!string.IsNullOrEmpty(item))
                {

                    var tmp = 0;
                    if (int.TryParse(item, out tmp))
                    {
                        result.Add(tmp);
                    }
                }
            }

            return result;
        }
        public static string IntsToString(this int num, char c = '|')
        {
            return c + num.ToString() + c;
        }

        public static string IntsToString(this List<int> list, char c = '|')
        {
            var result = string.Empty;

            if (list != null && list.Count > 0)
            {
                list = list.Distinct().ToList();
                foreach (int s in list)
                {
                    result += c + s.ToString();
                }

            }
            return result + c;
        }

        public static string ToListString(this List<string> list, char c = '|')
        {
            var result = string.Empty;

            if (list != null && list.Count > 0)
            {
                list = list.Distinct().ToList();
                foreach (string s in list)
                {
                    result += c + s.Trim();
                }

            }



            return result.Trim(c);
        }


        public static List<string> ToListString(this string input, char c = '|')
        {
            var result = new List<string>();
            foreach (var item in input.Split(c))
            {
                if (!string.IsNullOrEmpty(item))
                {
                    result.Add(item);
                }

            }
            return result;


        }




        public static string ToTimeAgo(this DateTime source)
        {
            return StringHelpers.TimeAgo(source);

        }
        public static DateTime ChangeTime(this DateTime dateTime, int hours, int minutes, int seconds, int milliseconds)
        {
            return new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                hours,
                minutes,
                seconds,
                milliseconds,
                dateTime.Kind);
        }
        public static string ToShortDescription(this string source)
        {
            const int length = 350;
            return source.Length > length ? source.Substring(0, length) + "..." : source;

        }
        public static string ToHits(this int source)
        {
            return StringHelpers.FormatNumber(source.ToString());
        }
        public static string ToPrice(this int source)
        {
            return StringHelpers.FormatNumber(source.ToString()) + " đ";
        }

        public static string ToSlug(this string input)
        {

            var output = StringHelpers.UnicodeUnSign(input).RemoveAccent().ToLower();
            // invalid chars           
            output = Regex.Replace(output, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            output = Regex.Replace(output, @"\s+", " ").Trim();
            // cut and trim 
            output = output.Substring(0, output.Length <= 45 ? output.Length : 45).Trim();
            output = Regex.Replace(output, @"\s", "-"); // hyphens   
            return output;
        }

        public static string RemoveAccent(this string input)
        {
            var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(input);
            return Encoding.ASCII.GetString(bytes);
        }
    }
}
