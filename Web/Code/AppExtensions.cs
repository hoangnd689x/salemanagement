using Core.Entities;
using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Web.Code
{
    public static class AppExtensions
    {

        public static string ToHiddenString(this string input)
        {

            if (input.Length > 3)
            {

                return "***" + input.Substring(2);
            }

            return "*****";
        }

        public static string ToPriceText(this decimal price)
        {
            var s = string.Format("{0:0.00}", price);

            if (s.EndsWith("00"))
            {
                return ((long)price).ToString("C").Substring(1);
            }
            else
            {
                return s;
            }
        }


        public static string ToHtmlLabel(this Published published)
        {
            var text = published.ToText();
            var label = "primary";
            if (published == Published.Show) label = "success";
            if (published == Published.Hide) label = "default";
            if (published == Published.Delete) label = "danger";

            return $"<span class='label label-{label} label-sm' >{text}</span>";
        }



       

        public static DateTime? ToViDate(this string input)
        {
            DateTime dt;
            if (DateTime.TryParseExact(input, "dd/MM/yyyy", null,
                                   DateTimeStyles.None,
                out dt))
            {
                //valid date
                return dt;
            }
            else
            {

                if (DateTime.TryParseExact(input, "d/M/yyyy", null,
                                   DateTimeStyles.None,
                out dt))
                {
                    //valid date
                    return dt;
                }
                //invalid date
            }
            return null;
        }


        public static string ToViDate(this DateTime dt)
        {
            //dt = dt.ToUniversalTime();
            return string.Format("{0:dd/MM/yyyy}", dt);
        }
        public static string ToViDateTime(this DateTime dt)
        {
            return string.Format("{0:HH:mm dd/MM/yyyy}", dt);
        }
        public static DateTime? ToViDateTime(this string input)
        {
            DateTime dt;
            if (DateTime.TryParseExact(input, "HH:mm dd/MM/yyyy", null, DateTimeStyles.None,
                out dt))
            {
                //valid date
                return dt;
            }
            else
            {
                //invalid date
            }
            return null;
        }


        public static string GetString(this IHtmlContent content)
        {
            //return new HtmlString(content.ToString());
            var writer = new System.IO.StringWriter();
            content.WriteTo(writer, HtmlEncoder.Default);

            return writer.ToString();
        }
    }
}
