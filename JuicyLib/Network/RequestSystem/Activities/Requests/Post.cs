using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http.Headers;

namespace JuicyLib.Network.Easy.Activities.Requests
{
    class Post : Request
    {
        //RequestParams parameters;
        Dictionary<string, string> post;

        public Post(string id) : base(id)
        {
        }

        public void SetPost(Dictionary<string, string> post)
        {
            this.post = post;
        }

        public void SetPost(String post)
        {
            this.post = new Dictionary<string, string>();

            string[] postSplit = post.Split('&');
            for (int i = 0; i < postSplit.Length; i++)
            {
                this.post.Add(postSplit[i].Split('=')[0], postSplit[i].Split('=')[1]);
            }
        }

        public override void Run(HttpClient client = null)
        {
            base.Run(client);

            var encodedContent = new FormUrlEncodedContent(post);

            try
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Writer.WriteStatus(id, "Sending post request");
                var response = client.PostAsync(url, encodedContent);
                result.SetValue(response.Result.Content.ReadAsStringAsync().ToString());
                Writer.WriteStatus(id, "Received a response");
            }
            catch (HttpRequestException ex)
            {
                Writer.WriteError($"{ex.Message}");
            }

            End();
        }

        public string PostMessage
        {
            get
            {
                string post = "";
                foreach (string key in this.post.Keys)
                {
                    post += $"{key}={this.post[key]}&";
                }

                post.Remove(post.Length - 1);
                return post;
            }
        }
    }
}
