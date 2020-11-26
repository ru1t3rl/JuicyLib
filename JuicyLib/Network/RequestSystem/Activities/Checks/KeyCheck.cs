using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JuicyLib.Network.Easy.Activities.Checks
{
    class KeyCheck : Activity
    {
        string value;
        List<string> succesKeys, failureKeys;

        public KeyCheck(string id) : base(id)
        {
        }

        public override void Run(HttpClient client = null)
        {
            base.Run(client);

            bool failure = false;

            for (int iKey = 0; iKey < failureKeys.Count; iKey++)
            {
                if (Regex.IsMatch(value, failureKeys[iKey]))
                {
                    failure = true;
                    break;
                }
            }

            if (failure)
            {
                result.SetValue(KeyCheckResults.FailureFound.ToString());
                Writer.WriteStatus(Id, Result);
                End();
                return;
            }

            failure = true;

            for (int iKey = 0; iKey < succesKeys.Count; iKey++)
            {
                if (Regex.IsMatch(value, succesKeys[iKey]))
                {
                    failure = false;
                    break;
                }
            }

            if (failure)
            {
                result.SetValue(KeyCheckResults.NoKeysFound.ToString());
                Writer.WriteError(Id, Result);
            }
            else
            {
                result.SetValue(KeyCheckResults.SuccessFound.ToString());
                Writer.WriteSucces(Id, Result);
            }

            End();
        }

        public void SetSuccessKeys(List<string> keys)
        {
            succesKeys = keys;
        }

        public void SetFailureKeys(List<string> keys)
        {
            failureKeys = keys;
        }

        public void SetToCheck(string value)
        {
            this.value = value;
        }
    }

    public enum KeyCheckResults
    {
        FailureFound,
        NoKeysFound,
        SuccessFound
    }
}
