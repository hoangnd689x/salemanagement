using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public static class IpHelper
    {
        public static async Task<string> GetCountryCodeByIpaddressAsync(string userip)
        {
            try
            {
                var url = "http://freegeoip.net/json/" + userip;
                var client = new HttpClient();
                var stringTask = await client.GetStringAsync(url);
                var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(stringTask);

                return (string)obj.country_code;
            }
            catch
            {

            }
            return "";
        }
    }
}
