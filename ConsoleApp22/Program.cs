using Newtonsoft.Json;
using PushbulletSharp;
using PushbulletSharp.Models.Requests;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp22
{
    class Program
    {
        private static double minimumprice = double.MaxValue;

        static void Main(string[] args)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            DateTime lastDate = DateTime.Now.AddDays(-1);
            
            while (true)
            {
                DateTime now = DateTime.Now;

                while (true)
                {
                    lastDate = now;
                    try
                    {
                        string url = "https://wolt.com/en/isr/tel-aviv/restaurant/nam-king-george";

                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                        request.UserAgent = "Mozilla/5.0 (Amiga; U; AmigaOS 1.3; en; rv:1.8.1.19) Gecko/20081204 SeaMonkey/1.1.14";
                        dynamic jsonObj = new object();
                        IWebProxy proxy = new WebProxy("zproxy.lum-superproxy.io", 22225);
                        string proxyUsername = @"lum-customer-socialboost-zone-mobile-country-us";
                        string proxyPassword = @"oxr1zri6cfhs";
                        proxy.Credentials = new NetworkCredential(proxyUsername, proxyPassword);
                        request.Proxy = proxy;
                        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                        using (Stream stream = response.GetResponseStream())
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            string jsonString = reader.ReadToEnd();


                            int count = Regex.Matches(jsonString, "The venue is temporarily offline").Count;

                            if (count == 1) 
                            {
                                var Client = new PushbulletClient("o.csiq6uFbgIejWZxujhIim6gqhZVshD5q");
                                PushNoteRequest reqeust = new PushNoteRequest()
                                {
                                    ChannelTag = "fqwfgnqwdlf",
                                    Title = "Nam",
                                    Body = "Open"
                                };

                                Client.PushNote(reqeust);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            
        }
    }
}
