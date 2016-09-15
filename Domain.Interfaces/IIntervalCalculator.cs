using System;

namespace Domain.Interfaces
{
    public interface IIntervalCalculator
    {
        TimeSpan ConfiguredSearchPeriod { get; set; }
        double CalculateTimeIntervals();
        double CalculateTimeIntervals(TimeSpan timeSpan);
    }
}