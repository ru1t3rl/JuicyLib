using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using xNet;

namespace JuicyLib.Network
{
    public static class SimplePOST
    {
        public static string Post(string url, NameValueCollection data)
        {
            using var client = new WebClient();
            var response = client.UploadValues(url, "POST", data);
            return Encoding.UTF8.GetString(response);
        }

        public static MatchCollection SIMPLE_Capture(string url, NameValueCollection data, Regex capture)
        {
            using var client = new WebClient();

            return capture.Matches(client.UploadValues(url, "POST", data).ToString());
        }

        public static MatchCollection SIMPLE_Capture(string url, NameValueCollection data, string captureRegex)
        {
            using var client = new WebClient();

            Regex capture = new Regex(captureRegex);
            return capture.Matches(client.UploadValues(url, "POST", data).ToString());
        }
    }

    public class POST
    {
        private string _userAgent;
        private string _proxy;

        public bool UseParameters = false;
        private readonly RequestParams _parameters;
        private readonly string _post;

        // No property yet
        private string _referer;


        private readonly HttpRequest _request;

        public POST(string post)
        {
            _post = post;
            _request = new HttpRequest();
            _parameters = new RequestParams();
        }

        // Url (Address)
        public string Address { get; set; }

        // UserAgent
        public string UserAgent
        {
            get => _userAgent;
            set
            {
                _userAgent = value;
                _request.UserAgent = value;
            }
        }

        // Referer
        public string Referer
        {
            get => _referer; 
            set
            {
                _referer = value;
                _request.Referer = _referer;
            }
        }

        // Proxy
        public string Proxy
        {
            set
            {
                _proxy = value;
                _request.Proxy = HttpProxyClient.Parse(_proxy);
            }
        }

        // Add POST Parameters
        public void AddParam(string key, string value)
        {
            _parameters[key] = value;
        }

        // Process POST-Request string to seperate Parameters
        private void ProcessPost()
        {
            string[] postParams = _post.Split(',');

            foreach (string p in postParams)
            {
                // Split the parameter to key and value replace the "
                _parameters[p.Split(':')[0].Replace("\"", "")] = p.Split(':')[1].Replace("\"", "");
            }
        }

        public String Request()
        {
            if (_post.Length > 1)
                ProcessPost();

            if (UserAgent == null || Address == null || Referer == null || _parameters.Count == 0)
            {
                return null;
            }
            else
            {
                try
                {
                    return _request.Post(Address, _parameters).ToString();
                }
                catch (HttpException ex)
                {
                    return ex.HttpStatusCode.ToString();
                }
            }
        }
    }
}
