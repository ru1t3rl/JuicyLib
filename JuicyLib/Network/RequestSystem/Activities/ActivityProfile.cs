using System;
using System.Collections.Generic;
using JuicyLib.Network.Easy.Activities.Requests;
using JuicyLib.Network.Easy.Activities.Checks;

namespace JuicyLib.Network.Easy.Activities
{
    public class ActivityInfo
    {
        public ActivityType type { get; set; }
        public string url { get; set; }
        public string post { get; set; }
        public string regex { get; set; }
        public Activity activity { get; set; }
        public List<string> succesKeys { get; set; }
        public List<string> failureKeys { get; set; }
        public string leftSubString { get; set; }
        public string rightSubString { get; set; }
        public string key { get; set; }
    }

    public class ActivityProfile
    {
        public List<ActivityInfo> activities { get; set; }

        public void InitActivities()
        {
            ActivityInfo info;

            for (int iActivity = 0; iActivity < activities.Count; iActivity++)
            {
                info = activities[iActivity];

                switch (activities[iActivity].type)
                {
                    case ActivityType.Activity:
                        SetupActivity(ref info);
                        break;
                    case ActivityType.Request:
                        SetupRequest(ref info);
                        break;
                    case ActivityType.Get:
                        SetupGet(ref info);
                        break;
                    case ActivityType.Post:
                        SetupPost(ref info);
                        break;
                    case ActivityType.Regex:
                        SetupRegex(ref info);
                        break;
                    case ActivityType.GetKey:
                        SetupGetKey(ref info);
                        break;
                    case ActivityType.Capture:
                        SetupCapture(ref info);
                        break;
                }

                activities[iActivity] = info;
            }
        }

        private void SetupActivity(ref ActivityInfo info)
        {
            info.activity = new Activity("Default");
        }

        private void SetupRequest(ref ActivityInfo info)
        {
            JuicyLib.Network.Easy.Activities.Requests.Request r = new JuicyLib.Network.Easy.Activities.Requests.Request("Request");
            r.SetUrl(info.url);
            info.activity = r;
        }

        private void SetupGet(ref ActivityInfo info)
        {
            Get g = new Get("Get");
            g.SetUrl(info.url);
            info.activity = g;
        }

        private void SetupPost(ref ActivityInfo info)
        {
            Post p = new Post("Post");
            p.SetUrl(info.url);
            p.SetPost(info.post);
            info.activity = p;
        }

        private void SetupRegex(ref ActivityInfo info)
        {
            MatchRegex mr = new MatchRegex("Regex");
            mr.SetRegex(info.regex);
            info.activity = mr;
        }

        private void SetupKeyCheck(ref ActivityInfo info)
        {
            KeyCheck kc = new KeyCheck("Key Check");
            kc.SetSuccessKeys(info.succesKeys);
            kc.SetFailureKeys(info.failureKeys);
            info.activity = kc;
        }

        private void SetupGetKey(ref ActivityInfo info)
        {
            GetKey c = new GetKey("Get Key");
            c.SetLeft(info.leftSubString);
            c.SetRight(info.rightSubString);
            c.SetKey(info.key);

            if (info.regex != null)
                c.SetRegex(info.regex);

            info.activity = c;
        }

        private void SetupCapture(ref ActivityInfo info)
        {
            Capture c = new Capture("Capture");
            c.SetLeft(info.leftSubString);
            c.SetRight(info.rightSubString);

            if (info.regex != null)
                c.SetRegex(info.regex);

            info.activity = c;
        }
    }
}
