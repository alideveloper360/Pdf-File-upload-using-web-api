using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace APIConsume
{
    public class GlobalVariable
    {
        public static HttpClient webapiclient = new HttpClient();
        static GlobalVariable()
        {
            webapiclient.BaseAddress = new Uri("http://localhost:60010/api/");
            webapiclient.DefaultRequestHeaders.Clear();
            webapiclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}