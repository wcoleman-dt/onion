namespace Domain.Entities
{
    public partial class ApiKeyType
    {
        public int Id { get; set; }
        public string EndpointType { get; set; }
        public string Description { get; set; }
    }
}
