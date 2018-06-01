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


        public Object CallService<T>(string serviceCall)
        {
            Request = WebRequest.Create(BaseUrl + serviceCall);
            try
            {
                Response = Request.GetResponse();
            }
            catch (Exception)
            {
                return null;
            }

            string json;
            using (var sr = new System.IO.StreamReader(Response.GetResponseStream()))
            {
                json = sr.ReadToEnd();
            }
            var temp = JsonConvert.DeserializeObject<T>(json);
            return temp;
        }

    }
}
