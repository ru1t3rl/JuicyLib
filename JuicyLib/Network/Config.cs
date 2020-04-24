using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace JuicyLib.Network
{
    public abstract class General
    {
        public double SchemeVersion { get; set; }
        public string CreatorName { get; set; }
        public string Note { get; set; }
        public int MaxThreads { get; set; }
    }

    public abstract class Request
    {
        public bool ProxyLess { get; set; }
        public string Method { get; set; }
        public string Url { get; set; }
        public string Referer { get; set; }
        public string UserAgent { get; set; }
        public string ContentType { get; set; }
        public string Accept { get; set; }
        public string PostData { get; set; }
        public List<Key> SuccesKeys { get; set; }
        public List<Key> FailureKeys { get; set; }
    }

    public class Config
    {
        public General General { get; set; }
        public List<Request> Requests { get; set; }
    }
}
