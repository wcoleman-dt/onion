namespace Domain.Entities
{
    public partial class ApiEndpoint
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string ApiKey { get; set; }
        public string Title { get; set; }
        public string Endpoint { get; set; }
        public string Curl { get; set; }
        public string NRSQLSyntax { get; set; }
        public int ApiKeyType { get; set; }
    }
}
