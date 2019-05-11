using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Code.StringHelper
{
    public static class StringHelpers
    {
        public static string GenerateRandom(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }      

    }
    public static class ToImages
    {
        public static string ToImage(this string Image)
        {
            var root = "http://statics.tvviet.us/Images/";

            return root + Image;
        }

    }
    public static class ToImages2
    {
        public static string ToImage2(this string Image)
        {
            var root = "/Resources/Images/";

            return root + Image;
        }

    }

    
}
