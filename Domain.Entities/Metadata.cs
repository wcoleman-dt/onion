using System.Collections.Generic;

namespace Domain.Entities
{
    public class Metadata
    {
        public int Id { get; set; }
        public List<string> eventTypes { get; set; }
        public string eventType { get; set; }
        public bool openEnded { get; set; }
        public string beginTime { get; set; }
        public string endTime { get; set; }
        public long beginTimeMillis { get; set; }
        public long endTimeMillis { get; set; }
        public string rawSince { get; set; }
        public string rawUntil { get; set; }
        public string rawCompareWith { get; set; }
        public string guid { get; set; }
        public string routerGuid { get; set; }
        //todo:inspect to see if a table is needed to capture these messages
        public List<object> messages { get; set; }
        public List<Content> contents { get; set; }
    }
}