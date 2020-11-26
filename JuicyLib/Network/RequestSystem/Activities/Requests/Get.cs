using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace JuicyLib.Network.Easy.Activities.Requests
{
    class Get : Request
    {
        public Get(string id) : base(id)
        {

        }

        public override void Run(HttpClient client = null)
        {
            base.Run(client);

            client = new HttpClient();

            Writer.WriteStatus(id, "Getting source code");
            var response = client.GetAsync(url);

            result.SetValue(response.Result.Content.ReadAsStringAsync().Result);
            Writer.WriteStatus(id, "Got the source Code");
            End();
        }
    }
}
