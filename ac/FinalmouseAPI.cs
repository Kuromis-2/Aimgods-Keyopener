using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using ac;
using Newtonsoft.Json.Linq;

namespace aimgods
{
    public class FinalmouseAPI
    {
        private string token;
        public FinalmouseAPI(string token, string u, string p)
        {
            if (token == null)
            {
                // try
                // {
                token = tokenGen(u, p);
                // }
                // catch
                // {
                //     Console.WriteLine(">> Error Login");
                // }
            }
            this.token = token;
        }
        string meURL = "https://production.api.finalmouse.com/aimgods/auth/me";
        public JObject me;
        public void meAPI()
        {
            var httpRequest = (HttpWebRequest)WebRequest.Create(meURL);

            httpRequest.Headers["Authorization"] = "Bearer " + token;

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                me = JObject.Parse(result);
            }
        }
        public string tokenGen(string un, string pw)
        {
            string result;
            var url = "https://production.api.finalmouse.com/aimgods/auth/login/web";
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/json";
            var data = "{\"username\":\"" + un + "\",\"pwd\":\"" + pw + "\"}";

            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }
            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            JObject json = JObject.Parse(result);
            var tokens = json["tokens"];
            var idtoken = tokens["id"];
            return Convert.ToString(idtoken);
        }

        string goldenKeyURL = "https://production.api.finalmouse.com/aimgods/golden-keys";
        public JObject goldenKeys;

        public void openKey(int amount,int delay,TextBox poggiesBox)//amount = amount of keys to open, delay = delay in ms between using GoldenKeys
        {
            for (int i = 0; i < amount; i++)
            {
                try
                {
                    var httpRequest = (HttpWebRequest)WebRequest.Create(goldenKeyURL);
                    httpRequest.Method = "POST";

                    httpRequest.Headers["Authorization"] = "Bearer " + token;
                    var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        goldenKeys = JObject.Parse(result);
                        if (poggiesBox.InvokeRequired == true)
                        {
                            poggiesBox.Invoke((MethodInvoker) delegate
                            {
                                poggiesBox.Text += "[" + i + "] " + result;
                                poggiesBox.Text += Environment.NewLine;
                            });
                        }
                        else
                        {
                            poggiesBox.Text += "[" + i + "] " + result;
                            poggiesBox.Text += Environment.NewLine;
                        }

                    }
                }
                catch
                {
                    poggiesBox.Text += "[" + i + "] "+"error";
                    poggiesBox.Text += Environment.NewLine;
                }
                Thread.Sleep(delay);
            }
            
        }
    }
}