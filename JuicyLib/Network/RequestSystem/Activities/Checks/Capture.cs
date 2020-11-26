using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JuicyLib.Network.Easy.Activities.Checks
{
    class Capture : Activity
    {
        Regex regex;
        string valueLeft;
        string valueRight;
        string value;

        public Capture(string id) : base(id)
        {
        }

        public override void Run(HttpClient client = null)
        {
            base.Run(client);

            if (regex == null)
                regex = new Regex($"{valueLeft}([a-zA-Z0-9,+=.\\]*){valueRight}");

            MatchCollection m = regex.Matches(value);
            List<string> hits = new List<string>();

            try
            {
                for (int i = 0; i < m.Count; i++)
                {
                    hits.Add(m[i].Groups[1].Value);
                }

                HitsToString(hits);

                result.SetValue(value);
                Writer.WriteSucces(Id, $"Finsihed with {hits.Count} captures");
            }
            catch (ArgumentOutOfRangeException)
            {
                Writer.WriteError(Id, "Could not capture anything");
                result.SetValue("");
            }

            End();
        }

        private void HitsToString(List<string> hits)
        {
            value = "";

            for (int i = 0; i < hits.Count; i++)
            {
                if (i != hits.Count - 1)
                    value += $"{hits[i]} | ";
                else
                    value += $"{hits[i]}";
            }
        }

        public void SetLeft(string value)
        {
            valueLeft = value;
        }

        public void SetRight(string value)
        {
            valueRight = value;
        }

        public void SetValue(string value)
        {
            this.value = value;
        }

        public void SetRegex(string value)
        {
            regex = new Regex(value);
        }
    }
}
