using System;
using System.Configuration;
using Domain.Entities;
using Domain.Interfaces;

namespace Domain.Utility
{
    public class IntervalCalculator : IIntervalCalculator
    {
        public IntervalCalculator()
        {
            var timePeriod = ConfigurationManager.AppSettings[CommonStrings.ConfiguredSearchPeriod];
            if (!string.IsNullOrEmpty(timePeriod))
            {
                ConfiguredSearchPeriod = TimeSpan.Parse(timePeriod);
            }
        }

        public TimeSpan ConfiguredSearchPeriod { get; set; }

        public double CalculateTimeIntervals()
        {
           return CalculateTimeIntervals(ConfiguredSearchPeriod);
        }

        public double CalculateTimeIntervals(TimeSpan timeSpan)
        {
            double retVal = 0;
            if (timeSpan.TotalMilliseconds > 0)
            {
                retVal = timeSpan.TotalMinutes ;
            }
            return retVal;
        }
    }
}
