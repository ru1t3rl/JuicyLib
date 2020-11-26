using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace JuicyLib.Network.Easy.Activities
{
    public class Activity
    {
        public Action onStart;

        /// <summary>
        /// Listens for the request to finish.
        /// </summary>
        public Action<Activity> onFinish;

        private bool isRunning;

        protected Result result;
        protected string id;

        public Activity(string id = "")
        {
            this.id = id;
        }

        public virtual void Run(HttpClient client = null)
        {
            Writer.WriteStatus(id, "Started activity");
            result = new Result();
        }

        protected virtual void End()
        {
            Writer.WriteStatus(id, "Finished activity");

            isRunning = false;
            onFinish?.Invoke(this);
        }

        public void SetId(string id)
        {
            this.id = id;
        }


        /// <summary>
        /// Returns the result/source code found with the request.
        /// </summary>
        public string Result => result.Value;

        /// <summary>
        /// Returns true if the request is on going.
        /// </summary>
        public bool IsBusy => isRunning;

        public string Id => id;
    }
}
