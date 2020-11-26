using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JuicyLib.Network.Easy.Activities.Checks
{
    class MatchRegex : Activity
    {
        Regex regex;
        string value;

        public MatchRegex(string id) : base(id)
        {
        }

        public void SetRegex(Regex regex)
        {
            this.regex = regex;
        }

        public void SetRegex(string regex)
        {
            this.regex = new Regex(regex);
        }

        public void SetToCheck(string value)
        {
            this.value = value;
        }

        public override void Run(HttpClient client = null)
        {
            base.Run(client);

            MatchCollection matches = regex.Matches(value);

            List<string> hits = new List<string>();
            for (int i = 0; i < matches.Count; i++)
            {
                hits.Add(matches[i].Value);
            }

            result.SetValue(hits);

            End();
        }
    }
}
