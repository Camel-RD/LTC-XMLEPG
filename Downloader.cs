using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Timers;
using System.Net.Http;


namespace LTC
{
    public class ADownloader
    {
        static ADownloader()
        {
            System.Net.WebRequest.DefaultWebProxy = null;
            ServicePointManager.DefaultConnectionLimit = 100;
            ServicePointManager.Expect100Continue = false;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        }

        public static async Task<string> GetString(string url, int timeout)
        {
            var hc = new HttpClient();
            //hc.BaseAddress = new Uri("https://tv.lattelecom.lv");
            hc.DefaultRequestHeaders.Add(
                        "User-Agent",
                        "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36");
            hc.Timeout = TimeSpan.FromMilliseconds(timeout);
            Task<string> task = hc.GetStringAsync(url);
            try
            {
                return await task;
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
                return null;
            }
            /*
            WebClient client = new WebClient() { Encoding = Encoding.UTF8 };
            Task<string> task = client.DownloadStringTaskAsync(url);
            try
            {
                if (await Task.WhenAny(task, Task.Delay(timeout)) == task)
                {
                    ret = task.Result;
                    return ret;
                }
                else
                {
                    client.CancelAsync();
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
             */
        }

    }
}
