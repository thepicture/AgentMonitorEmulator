using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;

namespace AgentMonitorEmulator.AgentMonitor_server.Controllers
{
    public class SrvController : ApiController
    {
        public static string code = "123456";
        public static string ServerRsaPublicKey = "654321";

        [Route("srv")]
        [AcceptVerbs("_CONNECT")]
        public HttpResponseMessage Connect()
        {
            try
            {
                string code = Request.Headers
                    .First(header => header.Key == "code").Value
                    .First();
                if (string.IsNullOrWhiteSpace(code))
                {
                    throw new Exception("code is not presented");
                }
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                response.Headers.Add("key", ServerRsaPublicKey);
                IEnumerable<char> guid = Guid
                    .NewGuid()
                    .ToString()
                    .Take(8);
                string agentid = string.Join("", guid);
                response.Headers.Add(nameof(agentid), agentid);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [Route("srv")]
        public HttpResponseMessage Options()
        {
            try
            {
                string key = Request.Headers
                    .First(header => header.Key == "key").Value
                    .First();
                if (string.IsNullOrWhiteSpace(key))
                {
                    throw new Exception("key is not presented");
                }
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                string token = Convert
                    .ToBase64String(
                        RSA
                            .Create()
                            .Encrypt(
                                Encoding.UTF8
                                    .GetBytes(key + ServerRsaPublicKey), RSAEncryptionPadding.OaepSHA1));
                response.Headers.Add(nameof(token), token);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
