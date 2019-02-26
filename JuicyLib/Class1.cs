using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;
using xNet;

namespace JuicyLib
{
    namespace Network
    {
        public static class GET
        {
            public static T Json<T>(string url) where T : new()
            {
                var data = string.Empty;
                using (WebClient client = new WebClient())
                {
                    try
                    {
                        data = client.DownloadString(url);

                    }
                    catch (WebException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    return !string.IsNullOrEmpty(data) ? JsonConvert.DeserializeObject<T>(data) : new T();
                }
            }

            public static string String(string url)
            {
                string data = "NULL";
                using (WebClient client = new WebClient())
                {
                    try
                    {
                        data = client.DownloadString(url);
                    }
                    catch (WebException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    return data;
                }
            }
        }

        public static class POST
        { 
            public static void SIMPLE(string url, NameValueCollection data)
            {
                using (var client = new WebClient())
                {
                    var repsone = client.UploadValues(url, "POST", data);
                    string responseString = Encoding.UTF8.GetString(response);
                }
            }

        }
    }

    namespace StringMechanic
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

                if(!url.Contains("www"))
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

                if (tempUrl.Contains("www.")) {
                    tempUrl = tempUrl.Replace("www.", "");
                }

                return (url.Contains("//")) ? url.Split('/')[0] + "//" + tempUrl : tempUrl;
            }
        }
    }
}
