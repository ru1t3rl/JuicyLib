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
using UnityEngine;

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
                    var response = client.UploadValues(url, "POST", data);
                    string responseString = Encoding.UTF8.GetString(response);
                }
            }

        }

        public class PostRequest
        {
            private string host;
            private string userAgent;
            private string proxy;

            public bool useParameters = false;
            private RequestParams parameters;
            private string post;

            // No property yet
            private string referer;


            private HttpRequest request;

            public PostRequest()
            {
                request = new HttpRequest();
                parameters = new RequestParams();
            }

            // Url (Address)
            public string Address
            {
                get { return host; }
                set { host = value; }
            }

            // UserAgent
            public string UserAgent
            {
                get { return userAgent; }
                set
                {
                    userAgent = value;
                    request.UserAgent = value;
                }
            }

            // Referer
            public string Referer
            {
                get { return referer; }
                set
                {
                    referer = value;
                    request.Referer = referer;
                }
            }

            // Proxy
            public string Proxy
            {
                set
                {
                    proxy = value;
                    request.Proxy = HttpProxyClient.Parse(proxy);
                }
            }

            // Add POST Parameters
            public void AddParam(string key, string value)
            {
                parameters[key] = value;
            }

            // Process POST-Request string to seperate Parameters
            private void processPOST()
            {
                string[] postParams = post.Split(',');

                foreach (string p in postParams)
                {
                    // Split the parameter to key and value replace the "
                    parameters[p.Split(':')[0].Replace("\"", "")] = p.Split(':')[1].Replace("\"", "");
                }
            }

            public String Request()
            {
                if (post.Length > 1)
                    processPOST();

                if (UserAgent == null || Address == null || Referer == null || parameters.Count == 0)
                {
                    return null;
                }
                else
                {
                    try
                    {
                        return request.Post(Address, parameters).ToString();
                    }
                    catch (HttpException ex)
                    {
                        return ex.HttpStatusCode.ToString();
                    }
                }
            }
        }
    }
}
