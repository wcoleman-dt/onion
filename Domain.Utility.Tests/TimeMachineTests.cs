using System;
using Domain.Interfaces;
using NUnit.Framework;

namespace Domain.Utility.Tests
{
    [TestFixture]
    public class TimeMachineTests
    {
        private TimeSpan _timeInterval = TimeSpan.Zero;

        [Test]
        public void timemachine_should_calculate_totalminutes_from_time_interval()
        {
            _timeInterval = TimeSpan.Parse("01:00:45:00");
            IIntervalCalculator intervalCalculator = new IntervalCalculator();
            var actual = intervalCalculator.CalculateTimeIntervals(_timeInterval);
            double expected = 1485D;

            Assert.NotNull(actual);
            Assert.IsTrue(actual == expected);
        }
    }

    
}
