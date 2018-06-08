using Newtonsoft.Json;
using System;
using System.Net;

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

    }
}
