using System;
using System.Net;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace JuicyLib.Network
{
    public static class GET
    {
        public static T Json<T>(string url, bool debug = true) where T : new()
        {
            var data = string.Empty;
            using (WebClient client = new WebClient())
            {
                try
                {
                    data = client.DownloadString(url);

                }
                catch (WebException ex)
                {
                    try
                    {
                        ConsoleColor fore = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ForegroundColor = fore;
                    }
                    catch
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                    }
                }

                return !string.IsNullOrEmpty(data) ? JsonConvert.DeserializeObject<T>(data) : new T();
            }
        }

        public static async Task<string> StringAsync(string url, bool debug = true)
        {
            string data = "NULL";
            using (WebClient client = new WebClient())
            {
                try
                {
                    data = await client.DownloadStringTaskAsync(new Uri(url));
                }
                catch (WebException ex)
                {
                    if (debug)
                    {
                        try
                        {
                            ConsoleColor fore = Console.ForegroundColor;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(ex.Message);
                            Console.ForegroundColor = fore;
                        }
                        catch
                        {
                            System.Windows.Forms.MessageBox.Show(ex.Message);
                        }
                    }
                }

                return data;
            }
        }

        public static string String(string url, bool debug = true)
        {
            string data = "NULL";
            using (WebClient client = new WebClient())
            {
                try
                {
                    data = client.DownloadString(new Uri(url));
                }
                catch (WebException ex)
                {
                    if (debug)
                    {
                        try
                        {
                            ConsoleColor fore = Console.ForegroundColor;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(ex.Message);
                            Console.ForegroundColor = fore;
                        }
                        catch
                        {
                            System.Windows.Forms.MessageBox.Show(ex.Message);
                        }
                    }
                }

                return data;
            }
        }

        public static string String(string url, string proxy, bool debug = true)
        {
            string data = "NULL";
            using (WebClient client = new WebClient())
            {
                try
                {
                    client.Proxy = new WebProxy(proxy);
                    data = client.DownloadString(new Uri(url));
                }
                catch (WebException ex)
                {
                    if (debug)
                    {
                        try
                        {
                            ConsoleColor fore = Console.ForegroundColor;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(ex.Message);
                            Console.ForegroundColor = fore;
                        }
                        catch
                        {
                            System.Windows.Forms.MessageBox.Show(ex.Message);
                        }
                    }
                }

                return data;
            }
        }

        public static async Task<string> StringAsync(string url, string proxy, bool debug = true)
        {
            string data = "NULL";
            using (WebClient client = new WebClient())
            {
                try
                {
                    client.Proxy = new WebProxy(proxy);
                    data = await client.DownloadStringTaskAsync(new Uri(url));
                }
                catch (WebException ex)
                {
                    try
                    {
                        ConsoleColor fore = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ForegroundColor = fore;
                    }
                    catch
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                    }
                }

                return data;
            }
        }

        public static string String(string url, string ip, int port, bool debug = true)
        {
            string data = "NULL";
            using (WebClient client = new WebClient())
            {
                try
                {
                    client.Proxy = new WebProxy(ip, port);
                    data = client.DownloadString(new Uri(url));
                }
                catch (WebException ex)
                {
                    if (debug)
                    {
                        try
                        {
                            ConsoleColor fore = Console.ForegroundColor;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(ex.Message);
                            Console.ForegroundColor = fore;
                        }
                        catch
                        {
                            System.Windows.Forms.MessageBox.Show(ex.Message);
                        }
                    }
                }

                return data;
            }
        }

        public static async Task<string> StringAsync(string url, string ip, int port, bool debug = true)
        {
            string data = "NULL";
            using (WebClient client = new WebClient())
            {
                try
                {
                    client.Proxy = new WebProxy(ip, port);
                    data = await client.DownloadStringTaskAsync(new Uri(url));
                }
                catch (WebException ex)
                {
                    try
                    {
                        ConsoleColor fore = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ForegroundColor = fore;
                    }
                    catch
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                    }
                }

                return data;
            }
        }
    }
}
