using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Domain.Utility
{
    public static class Get
    {
        public static string BaseUrl { get; set; }

        public static HttpResponseMessage HttpGet(string URI)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(StandardHttpContentTypes.ApplicationJson));
             HttpResponseMessage response = client.GetAsync(URI).Result;

            return response;
        }

        public static HttpResponseMessage NewRelicAPIHttpGet(string query)
        {
            //pull the account id

            //add the two http headers

            //encode the query


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(StandardHttpContentTypes.ApplicationJson));
            HttpResponseMessage response = client.GetAsync(query).Result;

            return response;
        }

    }
}

