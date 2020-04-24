using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuicyLib.StringMechanic
{
    public static class Url
    {
        public static string AddHttp(string url)
        {
            return (!url.Contains("http://") || !url.Contains("https://")) ? "http://" + url : url;
        }

        public static string AddHttps(string url)
        {
            return (!url.Contains("https://") || !url.Contains("http://")) ? "https://" + url : url;
        }

        public static string AddWWW(string url)
        {
            string tempUrl = url.Contains("http") ? url.Split('/')[2] : url;

            if (!url.Contains("www"))
                tempUrl = "www." + tempUrl;

            return (url.Contains("//")) ? url.Split('/')[0] + "//" + tempUrl : tempUrl;
        }

        public static string RemoveHttp(string url)
        {
            return url.Contains("http") ? url.Split('/')[2] : url;
        }

        public static string RemoveWWW(string url)
        {
            string tempUrl = url.Contains("http") ? url.Split('/')[2] : url;

            if (tempUrl.Contains("www."))
            {
                tempUrl = tempUrl.Replace("www.", "");
            }

            return (url.Contains("//")) ? url.Split('/')[0] + "//" + tempUrl : tempUrl;
        }
    }
}
