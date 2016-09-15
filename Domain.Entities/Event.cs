namespace Domain.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public bool IsDuplicate { get; set; }
        public byte[] HashedByteValue { get; set; }
        public string appName { get; set; }
        public double databaseDuration { get; set; }
        public double duration { get; set; }
        public string transactionType { get; set; }
        public double webDuration { get; set; }
        public string transactionSubType { get; set; }
        public double queueDuration { get; set; }
        public double externalDuration { get; set; }
        public int appId { get; set; }
        public string host { get; set; }
        public string name { get; set; }
        public int realAgentId { get; set; }
        //public string __invalid_name__response.status { get; set; }
        public long timestamp { get; set; }
        public string tripId { get; set; }
        public double? totalTime { get; set; }
        //public string __invalid_name__X-Bloom-ISF-Tenant { get; set; }
        public string errorType { get; set; }
        public string errorMessage { get; set; }
        public virtual Result Result { get; set; }
        public int ResultId { get; set; }
    }
}
