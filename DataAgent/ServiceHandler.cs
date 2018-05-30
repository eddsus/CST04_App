using Newtonsoft.Json;
using SharedDataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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


        public object CallService<T>(string serviceCall)
        {
            Request = WebRequest.Create(BaseUrl + serviceCall);
            Response = Request.GetResponse();

            string json;

            using (var sr = new System.IO.StreamReader(Response.GetResponseStream()))
            {

                json = sr.ReadToEnd().Normalize();
            }

            return JsonConvert.DeserializeObject<T>(json);
        }

    }
}
