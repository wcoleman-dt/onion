namespace Domain.Entities
{
    public class Content
    {
        public int Id { get; set; }
        public string function { get; set; }
        public int limit { get; set; }
        public Order order { get; set; }
    }
}