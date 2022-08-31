using AgentMonitorEmulator.AgentMonitor_PC.Models;
using System;
using System.Net;

namespace AgentMonitorEmulator.AgentMonitor_PC
{
    class Program
    {
        public static string code = "123456";
        public static string key = "4321";

        static void Main()
        {
            Dispatch();
        }

        public static void Dispatch()
        {
            try
            {
                SendConnectRequest();
                SendOptionsRequest();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.Read();
        }

        private static void SendConnectRequest()
        {
            Console.WriteLine(nameof(SendConnectRequest));
            WebRequest request = WebRequest.Create(Api.BaseUrl);
            request.Method = "_CONNECT";
            request.Headers.Add(nameof(code), code);
            WebResponse response = request.GetResponse();
            Console.WriteLine("got connect response: " + string.Join(", ", response.Headers.AllKeys));
        }

        private static void SendOptionsRequest()
        {
            Console.WriteLine(nameof(SendOptionsRequest));
            WebRequest request = WebRequest.Create(Api.BaseUrl);
            request.Method = "OPTIONS";
            request.Headers.Add(nameof(key), key);
            WebResponse response = request.GetResponse();
            Console.WriteLine("got options response: " + string.Join(", ", response.Headers.AllKeys));
        }
    }
}
