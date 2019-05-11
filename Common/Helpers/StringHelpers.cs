using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Common.Helpers
{
    public class StringHelpers
    {

        #region GenerateKey

        public static string UniqueNumber(int maxSize)
        {
            var chars = "1234567890".ToCharArray();
            var data = new byte[1];
            var crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[maxSize];
            crypto.GetNonZeroBytes(data);
            var result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length - 1)]);
            }
            return result.ToString();
        }
        public static string UniqueKey(int maxSize)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            var data = new byte[1];
            var crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[maxSize];
            crypto.GetNonZeroBytes(data);
            var result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length - 1)]);
            }
            return result.ToString();
        }

        public static string MD5Hash(string str)
        {
            byte[] buffer = MD5.Create().ComputeHash(Encoding.Default.GetBytes(str.ToLower()));
            var builder = new StringBuilder();
            foreach (byte t in buffer)
            {
                builder.AppendFormat("{0:x2}", t);
            }
            return builder.ToString();
        }

        public static byte[] SHA256HashToByte(string password)
        {
            SHA256Managed crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password), 0, Encoding.UTF8.GetByteCount(password));
            return crypto;
        }

        public static string SHA256Hash(string password)
        {
            SHA256Managed crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password), 0, Encoding.UTF8.GetByteCount(password));
            foreach (byte bit in crypto)
            {
                hash += bit.ToString("x2");
            }
            return hash;
        }
        public static string GenerateOrderCode()
        {
            var st = new DateTime(1970, 1, 1);
            var t = (DateTime.Now.ToUniversalTime() - st);
            var timestamp = (Int64)(t.TotalMinutes);
            string key = UniqueKey(6);
            return string.Format("{0}_{1}", key, timestamp);
        }


        #region Random
        //Function to get random number
        private static readonly Random Random = new Random();
        private static readonly object SyncLock = new object();
        public static int RandomNumber(int min, int max)
        {
            lock (SyncLock)
            { // synchronize
                return Random.Next(min, max);
            }
        }

        #endregion
        #endregion

        #region Base 64
        public static string Base64Encode(string data)
        {
            try
            {
                byte[] encDataByte = Encoding.UTF8.GetBytes(data);
                string encodedData = Convert.ToBase64String(encDataByte);
                return encodedData.Replace("+", "-").Replace("/", "_");
            }
            catch
            {
                return "";
            }
        }
        public static string Base64Decode(string data)
        {
            try
            {
                var encoder = new UTF8Encoding();
                var utf8Decode = encoder.GetDecoder();
                data = data.Replace("-", "+").Replace("_", "/");
                byte[] todecodeByte = Convert.FromBase64String(data);
                int charCount = utf8Decode.GetCharCount(todecodeByte, 0, todecodeByte.Length);
                var decodedChar = new char[charCount];
                utf8Decode.GetChars(todecodeByte, 0, todecodeByte.Length, decodedChar, 0);
                var result = new String(decodedChar);
                return result;
            }
            catch
            {
                return "";
            }
        }
        #endregion


        #region Filter


        public static string ContentFilter(string input)
        {
            input = System.Web.HttpUtility.HtmlDecode(input);
            var regx = new Regex(@"http(s)?://([\w+?\.\w+])+([a-zA-Z0-9\~\!\@\#\$\%\^\&amp;\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]*)?", RegexOptions.IgnoreCase);
            MatchCollection mactches = regx.Matches(input);
            input = mactches.Cast<Match>().Aggregate(input, (current, match) => current.Replace(match.Value, "<a href='" + match.Value + "' target='_blank'>" + match.Value + "</a>"));

            input = input.Replace("\n", "<br/>");

            return input;
        }

        public static string FilterYahooSmiley(string input)
        {

            //Smiley http://messenger.yahoo.com/features/emoticons/

            input = input.Replace(":-??", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/106.gif\">");
            input = input.Replace("%-(", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/107.gif\">");
            input = input.Replace(":@)", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/49.gif\">");
            input = input.Replace("@};-", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/53.gif\">");
            input = input.Replace(":-\"", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/65.gif\">");
            input = input.Replace("b-(", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/66.gif\">");
            input = input.Replace("B-(", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/66.gif\">");
            input = input.Replace(":)>-", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/67.gif\">");
            input = input.Replace("[-X", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/68.gif\">");
            input = input.Replace("[-x", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/68.gif\">");
            input = input.Replace(@"\:D/", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/69.gif\">");
            input = input.Replace(@"\:d/", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/69.gif\">");
            input = input.Replace(">:/", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/70.gif\">");
            input = input.Replace(";))", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/71.gif\">");
            input = input.Replace(":-@", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/76.gif\">");
            input = input.Replace("^:)^", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/77.gif\">");
            input = input.Replace(":-j", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/78.gif\">");
            input = input.Replace(":-J", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/78.gif\">");


            //*------------------------

            input = input.Replace("#:-s", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/18.gif\">");
            input = input.Replace("#:-S", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/18.gif\">");
            input = input.Replace(":)]", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/100.gif\">");
            input = input.Replace(">:)", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/19.gif\">");
            input = input.Replace(":((", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/20.gif\">");
            input = input.Replace(":))", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/21.gif\">");
            input = input.Replace("<):)", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/48.gif\">");
            input = input.Replace(";;)", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/5.gif\">");
            input = input.Replace(">:s<", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/6.gif\">");
            input = input.Replace(">:S<", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/6.gif\">");
            input = input.Replace(":-ss", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/42.gif\">");
            input = input.Replace(":-SS", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/42.gif\">");
            input = input.Replace("/:)", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/23.gif\">");
            input = input.Replace("~x(", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/102.gif\">");
            input = input.Replace("~X(", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/102.gif\">");
            input = input.Replace(":\">", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/9.gif\">");
            input = input.Replace(":-*", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/11.gif\">");
            input = input.Replace(":*", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/11.gif\">");
            input = input.Replace("=((", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/12.gif\">");
            input = input.Replace(":-o", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/13.gif\">");
            input = input.Replace(":-O", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/13.gif\">");
            input = input.Replace(":o", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/13.gif\">");
            input = input.Replace(":O", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/13.gif\">");
            input = input.Replace("x(", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/14.gif\">");
            input = input.Replace("X(", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/14.gif\">");
            input = input.Replace("x-(", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/14.gif\">");
            input = input.Replace("X-(", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/14.gif\">");
            input = input.Replace(":->", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/15.gif\">");
            input = input.Replace(":>", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/15.gif\">");
            input = input.Replace("x-)", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/16.gif\">");
            input = input.Replace("X-)", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/16.gif\">");
            input = input.Replace("=))", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/24.gif\">");
            input = input.Replace("o:-)", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/25.gif\">");
            input = input.Replace("O:-)", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/25.gif\">");
            input = input.Replace(":-b", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/26.gif\">");
            input = input.Replace(":-B", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/26.gif\">");
            input = input.Replace("=;", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/27.gif\">");
            input = input.Replace(":-c", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/101.gif\">");
            input = input.Replace(":-C", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/101.gif\">");
            input = input.Replace(":-h", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/103.gif\">");
            input = input.Replace(":-H", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/103.gif\">");
            input = input.Replace(":-t", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/104.gif\">");
            input = input.Replace(":-T", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/104.gif\">");
            input = input.Replace("8->", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/105.gif\">");
            input = input.Replace("i-)", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/28.gif\">");
            input = input.Replace("I-)", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/28.gif\">");
            input = input.Replace("8-|", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/29.gif\">");
            input = input.Replace("l-)", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/30.gif\">");
            input = input.Replace("L-)", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/30.gif\">");
            input = input.Replace(":-&", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/31.gif\">");
            input = input.Replace(":-$", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/32.gif\">");
            input = input.Replace("[-(", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/33.gif\">");

            input = input.Replace(":o)", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/34.gif\">");
            input = input.Replace(":O)", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/34.gif\">");
            input = input.Replace("8-}", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/35.gif\">");
            input = input.Replace("<:-p", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/36.gif\">");
            input = input.Replace("<:-P", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/36.gif\">");
            input = input.Replace("(:|", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/37.gif\">");
            input = input.Replace("=p~", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/38.gif\">");
            input = input.Replace("=P~", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/38.gif\">");
            input = input.Replace(":-?", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/39.gif\">");
            input = input.Replace("#-o", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/40.gif\">");
            input = input.Replace("#-O", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/40.gif\">");

            input = input.Replace("=d>", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/41.gif\">");
            input = input.Replace("=D>", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/41.gif\">");
            input = input.Replace("@-)", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/43.gif\">");
            input = input.Replace(":^o", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/44.gif\">");
            input = input.Replace(":^O", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/44.gif\">");
            input = input.Replace(":-w", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/45.gif\">");
            input = input.Replace(":-W", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/45.gif\">");
            input = input.Replace(":-<", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/46.gif\">");
            input = input.Replace(">:p", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/47.gif\">");
            input = input.Replace(">:P", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/47.gif\">");

            input = input.Replace("X_X", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/109.gif\">");
            input = input.Replace("x_x", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/109.gif\">");
            input = input.Replace(":!!", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/110.gif\">");

            input = input.Replace(@"\m/", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/111.gif\">");
            input = input.Replace(@"\M/", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/111.gif\">");
            input = input.Replace(":-q", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/112.gif\">");
            input = input.Replace(":-Q", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/112.gif\">");
            input = input.Replace(":-bd", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/113.gif\">");
            input = input.Replace(":-BD", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/113.gif\">");
            input = input.Replace("^#(^", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/114.gif\">");
            input = input.Replace(":ar!", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/115.gif\">");
            input = input.Replace(":AR!", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/115.gif\">");
            input = input.Replace(":)", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/1.gif\">");
            input = input.Replace(":(", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/2.gif\">");
            input = input.Replace(";)", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/3.gif\">");
            input = input.Replace(":D", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/4.gif\">");
            input = input.Replace(":d", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/4.gif\">");
            input = input.Replace(":|", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/22.gif\">");
            input = input.Replace(":-S", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/17.gif\">");
            input = input.Replace(":-s", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/17.gif\">");
            input = input.Replace(":-/", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/7.gif\">");
            input = input.Replace(":s", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/7.gif\">");
            input = input.Replace(":S", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/7.gif\">");
            input = input.Replace(":x", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/8.gif\">");
            input = input.Replace(":X", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/8.gif\">");
            input = input.Replace(":P", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/10.gif\">");
            input = input.Replace(":p", "<img src=\"http://l.yimg.com/us.yimg.com/i/mesg/emoticons7/10.gif\">");
            return input;
        }

        public static string ParseLinkByText(string txt)
        {
            var regx = new Regex(@"http(s)?://([\w+?\.\w+])+([a-zA-Z0-9\~\!\@\#\$\%\^\&amp;\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]*)?", RegexOptions.IgnoreCase);

            MatchCollection mactches = regx.Matches(txt);
            if (mactches.Count > 0)
            {
                return mactches[0].Value;
            }
            return string.Empty;
        }

        public static string RemoveSpecialCharacters(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            var r = new Regex("(?:[^a-z0-9 -]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            string output = r.Replace(input, String.Empty);
            return output;
        }


        public static string ParseUrlText(string intput)
        {
            intput = UnicodeUnSign(intput);
            intput = RemoveSpecialCharacters(intput);
            intput = intput.Replace(" ", "-");
            intput = intput.ToLower();
            return intput;
        }

        #endregion

        #region  Other
        public static string TimeAgo(DateTime? timeInput, out double min)
        {
            string result = "";
            min = 0;
            if (timeInput.HasValue)
            {
                DateTime startDate = DateTime.Now;
                TimeSpan deltaMinutes = startDate.Subtract(timeInput.Value);

                double minutes = deltaMinutes.TotalMinutes;
                min = minutes;
                //int mi = (minutes);
                if (minutes < 1)
                {
                    result = "vài giây trước";
                }
                else if (minutes < 50)
                {
                    result = Math.Round(new decimal(minutes)) + " phút trước";
                }
                //else if (minutes < 90)
                //{
                //    result = "một giờ trước";
                //}
                else if (minutes < 1080)
                {
                    result = Math.Round(new decimal((minutes / 60))) + " giờ trước";
                }
                //else if (minutes < 1440)
                //{
                //    result = "một ngày trước";
                //}
                //else if (minutes < 2880)
                //{
                //    result = "about one day";
                //}
                else
                {
                    result = Math.Round(new decimal((minutes / 1440))) + " ngày trước";
                }
            }
            return result;

        }
        public static string TimeAgo(DateTime? timeInput)
        {
            string result = "";
            if (timeInput.HasValue)
            {
                DateTime startDate = DateTime.Now;
                TimeSpan deltaMinutes = startDate.Subtract(timeInput.Value);

                double minutes = deltaMinutes.TotalMinutes;
                //int mi = (minutes);
                if (minutes < 1)
                {
                    result = "vài giây trước";
                }
                else if (minutes < 50)
                {
                    result = Math.Round(new decimal(minutes)) + " phút trước";
                }
                //else if (minutes < 90)
                //{
                //    result = "một giờ trước";
                //}
                else if (minutes < 1080)
                {
                    result = Math.Round(new decimal((minutes / 60))) + " giờ trước";
                }
                //else if (minutes < 1440)
                //{
                //    result = "một ngày trước";
                //}
                //else if (minutes < 2880)
                //{
                //    result = "about one day";
                //}
                else
                {
                    result = Math.Round(new decimal((minutes / 1440))) + " ngày trước";
                }
            }
            return result;

        }

        public static string GetStringBetween(string str, string seq, string seqEnd)
        {
            string orgi = str;
            try
            {
                str = str.ToLower();
                seq = seq.ToLower();
                seqEnd = seqEnd.ToLower();

                int i = str.IndexOf(seq);

                if (i < 0)
                    return "";

                i = i + seq.Length;

                int j = str.IndexOf(seqEnd, i);
                int end;

                if (j > 0) end = j - i;
                else end = str.Length - i;

                return orgi.Substring(i, end);
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string GetStringsByList(List<int> list)
        {
            return list.Aggregate("", (current, i) => current + ("_" + i));
        }

        public static List<string> GetStringsBetween(string input, string start, string end)
        {
            string orgInput = input;
            input = input.ToLower();
            start = start.ToLower();
            end = end.ToLower();
            var list = new List<string>();
            try
            {
                int index = 0;
                while (index >= 0)
                {
                    index = input.IndexOf(start);
                    if (index < 0)
                        break;

                    index += start.Length;

                    int j = input.IndexOf(end, index);
                    if (j > 0)
                    {
                        int length = j - index;
                        list.Add(orgInput.Substring(index, length));
                        if (j + end.Length < input.Length)
                        {
                            input = input.Substring(j + end.Length);
                            orgInput = orgInput.Substring(j + end.Length);
                        }
                        else break;
                    }
                    else
                        break;
                }
                return list;
            }
            catch
            {
                return null;
            }
        }

        public static string PathbyDate()
        {
            return DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day;
        }

        public static string FormatNumber(string input)
        {
            const string commaDelimiter = ".";
            //if(isNaN(input)) return ;
            const string pattern = "(-?[0-9]+)([0-9]{3})";
            var regex = new Regex(pattern);

            while (Regex.IsMatch(input, pattern))
            {
                input = regex.Replace(input, "$1" + commaDelimiter + "$2");
            }
            return input;
        }

  


        public static string ExtractString(string source, int length)
        {
            if (string.IsNullOrEmpty(source))
                return "";
            source = source.Trim();
            string dest = String.Empty;
            if (length == 0 || source.Length == 0)
                return dest;
            if (source.Length < length)
                dest = source;
            else
            {
                string tmp = source.Substring(0, length);
                int nSub = tmp.Length - 1;
                while (tmp[nSub] != ' ')
                {
                    nSub--;
                    if (nSub == 0) break;
                }
                dest = tmp.Substring(0, nSub);
            }
            return dest;
        }
        public static string ParseTimeByDuration(int duration)
        {
            string strDuration = ((decimal)((duration / 60))).ToString().PadLeft(2, '0') + ":" + (duration % 60).ToString().PadLeft(2, '0');
            return strDuration;
        }



        public static string ParseFileSize(int filesize)
        {
            string strFileSize = (Math.Round(filesize / (1024.0 * 1024), 2)).ToString() + " MB";
            return strFileSize;
        }
        #endregion

        public static bool IsImage(string contenttype)
        {
            contenttype = contenttype.ToLower();

            var listImageContentType = new List<string>() { "image/jpg", "image/jpeg", "image/pjeg", "image/gif", "image/x-png", "image/png" };

            foreach (var item in listImageContentType /*SettingValues.ImageContentType*/)
            {
                if (item == contenttype) return true;
            }
            //return SettingValues.ImageContentType.Contains(contenttype.ToLower());
            return false;

        }

        public static string HtmlTrim(string input)
        {
            input = input.Trim();
            var decode = HttpUtility.HtmlDecode(input);
            var encode = HttpUtility.HtmlEncode(decode);

            var builder = new StringBuilder();
            foreach (char c in encode)
            {
                if ((int)c > 127)
                {
                    builder.Append("&#");
                    builder.Append((int)c);
                    builder.Append(";");
                }
                else
                {
                    builder.Append(c);
                }
            }
            return HttpUtility.HtmlDecode(builder.ToString()).Trim();
        }

        public static string UnicodeUnSign(string s)
        {
            const string uniChars = "àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệđìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆĐÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴÂĂĐÔƠƯ";
            const string koDauChars = "aaaaaaaaaaaaaaaaaeeeeeeeeeeediiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAAEEEEEEEEEEEDIIIOOOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAADOOU";

            if (string.IsNullOrEmpty(s))
            {
                return s;
            }
            string retVal = String.Empty;
            for (int i = 0; i < s.Length; i++)
            {
                int pos = uniChars.IndexOf(s[i].ToString());
                if (pos >= 0)
                    retVal += koDauChars[pos];
                else
                    retVal += s[i];
            }
            return retVal;
        }

        public static IEnumerable<string> GetSubStrings(string input, string start, string end)
        {
            var r = new Regex(Regex.Escape(start) + "(.*?)" + Regex.Escape(end));
            var matches = r.Matches(input);
            foreach (Match match in matches)
                yield return match.Groups[1].Value;
        }

        public static string RemoveUrlParram(string url)
        {
            var idx = url.IndexOf("?id", System.StringComparison.Ordinal);
            if (idx > 0)
            {
                return url.Substring(0, idx);
            }
            return url;
        }

        public static string GetHexTime()
        {
            var epochTime = new DateTime(1970, 1, 1, 0, 0, 0);
            TimeSpan diff = DateTime.UtcNow - epochTime;
            return ((long)diff.TotalSeconds).ToString("x");
        }

    }
}
