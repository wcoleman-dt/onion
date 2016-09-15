using System.Collections.Generic;

namespace Domain.Entities
{
    public class Result
    {
        public Result()
        {
            events = new List<Event>();
        }
        public int Id { get; set; }
        public ICollection<Event> events { get; set; }
        public virtual RootObject RootObject { get; set; }
        public int RootObjectId { get; set; }
    }
}