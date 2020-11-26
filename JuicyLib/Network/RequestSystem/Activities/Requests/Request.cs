using System;
using System.Net.Http;
using System.Threading.Tasks;


namespace JuicyLib.Network.Easy.Activities.Requests
{
    class Request : Activity
    {
        protected string url;


        protected HttpClient client;

        public Request(string id = "") : base(id)
        {
        }

        public override void Run(HttpClient client = null)
        {
            base.Run(client);

            this.client = client;

            if (client == null)
            {
                HttpClientHandler handler = new HttpClientHandler();
                handler.AllowAutoRedirect = true;
                this.client = new HttpClient(handler);
            }

            
        }

        protected override void End()
        {
            base.End();
        }

        public void SetUrl(string url)
        {
            this.url = url;
        }

        public string Url => url;
        public HttpClient Client => client;
    }
}
