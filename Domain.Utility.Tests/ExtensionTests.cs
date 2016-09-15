using Domain.Entities;
using Domain.HashingAlgorithm;
using Domain.Interfaces;
using NUnit.Framework;

namespace Domain.Utility.Tests
{
    [TestFixture]
    public class ExtensionTests
    {
        [Test]
        public void event_should_generate_hash()
        {
            Event eventObj = new Event()
            {
                appId = 1,
                appName="Test App",
                databaseDuration = 23.44F,
                duration = 55.4F,
                errorMessage = "Big Error!",
                errorType = "Bad Kind",
                host = "wdoleman-tt",
                name = "Test",
                externalDuration = 8.44F,
                queueDuration = 11.22F,
                realAgentId = 10,
                timestamp = 45698745632,
                totalTime = null,
            };
            IHashingAlgorithm hashingAlgorithm = new Murmur3();
            var result = eventObj.IterateOverEventExecutingAction(hashingAlgorithm);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length > 5);
        }
    }
}
