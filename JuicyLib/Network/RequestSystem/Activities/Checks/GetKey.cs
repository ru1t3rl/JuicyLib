using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace JuicyLib.Network.Easy.Activities.Checks
{
    class GetKey : Activity
    {
        Regex regex;
        string valueLeft;
        string valueRight;
        string value;
        string key;

        public GetKey(string id) : base(id)
        {
        }

        public override void Run(HttpClient client = null)
        {
            base.Run(client);

            if (regex == null)
                regex = new Regex($"{valueLeft}([a-zA-Z0-9,+=.\\]*){valueRight}");
            Match m = regex.Match(value);

            try
            {
                result.SetValue(m.Groups[1].Value);
            }
            catch (ArgumentOutOfRangeException)
            {
                Writer.WriteError(Id, "Could not capture anything");
                result.SetValue("");
            }

            End();
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

        public void SetKey(string value)
        {
            key = value;
        }

        public void SetRegex(string value)
        {
            regex = new Regex(value);
        }

        public string Key => key;
    }
}
