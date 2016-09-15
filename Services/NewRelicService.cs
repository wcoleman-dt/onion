using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Utility;
using Newtonsoft.Json;
using Services.Interfaces;

namespace Services.NewRelic
{
    public class NewRelicService : INewRelicService
    {
        private readonly IApiEndPointRepository _apiEndpointRepository;
        const int APIKEYTYPE = 2;
        const string X_QUERYKEY_HEADER = CommonStrings.XQueryKey;

        public NewRelicService(IApiEndPointRepository apiEndpointRepository)
        {
            _apiEndpointRepository = apiEndpointRepository;
        }

        public RootObject GetTransactionsSinceLastInterval(TimeSpan sinceLastDateTime, int resultLimit)
        {
            //get the APIEndPoint type = Query Key
            var queryKeyApiEndpoint = _apiEndpointRepository.GetApiEndpoints(APIKEYTYPE).First();
            var encodedQuery = string.Empty;
            if (!string.IsNullOrEmpty(queryKeyApiEndpoint.NRSQLSyntax))
            {
                var apiQuery = string.Format(queryKeyApiEndpoint.NRSQLSyntax, sinceLastDateTime.TotalMinutes,
                    resultLimit);
                encodedQuery = apiQuery.HtmlEncode();
                Debug.WriteLine(encodedQuery);
            }
            var result = NewRelicService.HttpGet(queryKeyApiEndpoint, encodedQuery);
            var stringResult =  result?.Content?.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<RootObject>(stringResult?.Result);
            return results;
        }

        private static HttpResponseMessage HttpGet(ApiEndpoint apiEndpoint, string encodedQuery)
        {
            var url = string.Concat(apiEndpoint.Endpoint, encodedQuery);
            HttpClient client = new HttpClient {BaseAddress = new Uri(url)};
            client.DefaultRequestHeaders.Add(X_QUERYKEY_HEADER, apiEndpoint.ApiKey );
            client.DefaultRequestHeaders.Add(CommonStrings.Accept, StandardHttpContentTypes.ApplicationJson);
            HttpResponseMessage response = client.GetAsync(url).Result;

            return response;
        }
    }

   
}