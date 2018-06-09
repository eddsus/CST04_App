using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;

namespace DataAgent
{
    public class ServiceHandler
    {
        private string BaseUrl { get; set; }
        private WebRequest Request { get; set; }
        private WebResponse Response { get; set; }

        public ServiceHandler(string baseUrl)
        {
            BaseUrl = baseUrl;
        }

        /***
         * Returns null if Service is not available, else returns T
         * ***/
        public T CallService<T>(string serviceCall)
        {
            Request = WebRequest.Create(BaseUrl + serviceCall);
            try
            {
                Response = Request.GetResponse();
            }
            catch (Exception)
            {
                return default(T);
            }

            string json;
            using (var sr = new System.IO.StreamReader(Response.GetResponseStream()))
            {
                json = sr.ReadToEnd();
            }
            //::TODO::remove temp and return directly, leaving it now for debugging
            var temp = JsonConvert.DeserializeObject<T>(json);
            return temp;
        }

        public bool CallUpdateService<T>(string serviceCall, T item)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream mem = new MemoryStream();
            ser.WriteObject(mem, item);
            string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
            WebClient webClient = new WebClient();
            webClient.Headers["Content-type"] = "application/json";
            webClient.Encoding = Encoding.UTF8;
            try
            {
                webClient.UploadString(BaseUrl + serviceCall, "POST", data);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
