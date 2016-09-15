namespace Domain.Entities
{
    public class NewRelicHttpRequest
    {
        public ApiEndpoint ApiEndpoint { get; set; }
        public RootObject RootObject { get; set; }
        public string NrsqlQuery { get; set; }

        public NewRelicHttpRequest() { }

        public NewRelicHttpRequest(ApiEndpoint apiEndpoint, RootObject rootObject, string nRsqlQuery)
        {
            ApiEndpoint = apiEndpoint;
            RootObject = rootObject;
            NrsqlQuery = nRsqlQuery;
        }
    }
}