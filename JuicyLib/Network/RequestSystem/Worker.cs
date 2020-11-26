using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using JuicyLib.Network.Easy.Activities;
using JuicyLib.Network.Easy.Activities.Checks;
using JuicyLib.Network.Easy.Activities.Requests;

namespace JuicyLib.Network.Easy
{
    public class Worker
    {
        ActivityProfile profile;
        string id;

        List<String> results;

        public Action<Worker> onFinish;

        Boolean stop = false;

        Dictionary<string, string> keyValues;

        public Worker(string id, ActivityProfile profile)
        {
            this.profile = profile;
            this.id = id;
        }

        public void AddRequest(Activity activity)
        {

        }

        public void Stop()
        {
            stop = true;
        }

        public async Task<List<String>> Run()
        {
            keyValues = new Dictionary<string, string>();
            results = new List<string>();
            Writer.WriteStatus(id, "Just started working");

            profile.InitActivities();
            Writer.WriteStatus(id, "Initialized Activities");

            for (int i = 0; i < profile.activities.Count(); i++)
            {

                profile.activities[i].activity.onFinish += onFinishRequest;

                switch (profile.activities[i].type)
                {
                    case ActivityType.Regex:
                        MatchRegex mr = profile.activities[i].activity as MatchRegex;
                        mr.SetToCheck(results[results.Count - 1]);
                        break;
                    case ActivityType.KeyCheck:
                        KeyCheck kc = profile.activities[i].activity as KeyCheck;
                        kc.SetToCheck(results[results.Count - 1]);
                        break;
                    case ActivityType.Capture:
                        Capture c = profile.activities[i].activity as Capture;
                        c.SetValue(results[results.Count - 1]);
                        break;
                    case ActivityType.GetKey:
                        GetKey gk = profile.activities[i].activity as GetKey;
                        gk.SetValue(results[results.Count - 1]);
                        break;
                    case ActivityType.Post:
                        Post p = profile.activities[i].activity as Post;
                        string pMessage = p.PostMessage;
                        string url = p.Url;
                        ReplaceWithKeyValue(ref pMessage);
                        ReplaceWithKeyValue(ref url);
                        p.SetPost(pMessage);
                        p.SetUrl(url);
                        break;
                    case ActivityType.Get:
                        Get g = profile.activities[i].activity as Get;
                        string u = g.Url;
                        ReplaceWithKeyValue(ref u);
                        g.SetUrl(u);
                        break;
                }

                profile.activities[i].activity.Run();

                if (stop)
                    break;
            }

            onFinish?.Invoke(this);
            return results;
        }

        public void ReplaceWithKeyValue(ref string value)
        {
            foreach (string key in keyValues.Keys)
            {
                value.Replace($"<{key}>", keyValues[key]);
            }
        }

        public void onFinishRequest(Activity activity)
        {
            if (activity.GetType() == typeof(KeyCheck))
            {
                KeyCheck kc = activity as KeyCheck;
                if (activity.Result == KeyCheckResults.NoKeysFound.ToString() ||
                    activity.Result == KeyCheckResults.FailureFound.ToString())
                    stop = true;
            }
            else if (activity.GetType() == typeof(GetKey))
            {
                GetKey c = activity as GetKey;

                keyValues.Add(c.Key, c.Result);
            }

            results.Add(activity.Result);
            Writer.WriteDebug(id, activity.Result);
        }

        public List<string> Result => results;
        public string Id => id;
    }
}
