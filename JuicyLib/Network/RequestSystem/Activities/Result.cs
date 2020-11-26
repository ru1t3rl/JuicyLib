using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JuicyLib.Network.Easy.Activities
{
    public class Result
    {
        string value = "";

        public void SetValue(List<string> values)
        {
            for(int i = 0; i < values.Count; i++)
            {
                if (i < values.Count - 1)
                    value += $"{values[i]}|";
                else
                    value += value;
            }
        }

        public void SetValue(string value)
        {
            this.value = value;
        }

        public string Value => value;
    }
}
