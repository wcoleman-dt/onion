using System.Collections.Generic;

namespace Domain.Entities
{
    public class RootObject
    {
        public int Id { get; set; }
        public virtual ICollection<Result> results { get; set; } = new List<Result>();
        public virtual PerformanceStats performanceStats { get; set; }
        public virtual Metadata metadata { get; set; }
    }
}