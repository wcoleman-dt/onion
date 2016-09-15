using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.HashingAlgorithm;
using Domain.Interfaces;
using Domain.Utility;
using NUnit.Framework;

namespace Infrastructure.Data.Tests
{
    [TestFixture]
    public class RootObjectTests
    {
        readonly IRootObjectRepository _rootObjectRepository = new RootObjectRepository();

        [Test]
        public void rootObject_should_save_to_database()
        {
            RootObject rootObject = new RootObject();

            var result = _rootObjectRepository.SaveRootObject(rootObject);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Id > 0);
        }

        [Test]
        public void rootObject_objectGraph_should_save_to_database()
        {
            RootObject rootObject = new RootObject();
            rootObject.metadata = new Metadata()
            {
                contents = new List<Content>()
                {
                    new Content()
                    {
                        function = GenerateRandomHashValue(),
                        limit=1,
                        order = new Order()
                        {
                            column = GenerateRandomHashValue(),
                            descending = true
                        }
                    }
                },
                beginTime = DateTime.Now.ToShortDateString(),
                endTime = DateTime.Now.ToShortDateString(),
                endTimeMillis = DateTime.Now.AddMinutes(5).Ticks,
                guid = Guid.NewGuid().ToString()
            };
            rootObject.performanceStats = new PerformanceStats()
            {
                cacheMisses = 5,
                decompressedBytes = 4,
                fileProcessingTime = 3,
                cacheSkipped = 2,
                decompressionTime = 1
            };
            rootObject.results = new List<Result>()
            {
                new Result()
                {
                 events   = new List<Event>()
                 {
                     new Event()
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
                        timestamp = DateTime.Now.Ticks,
                        totalTime = null
                     }
                 },
                }
            };
            IHashingAlgorithm hashingAlgorithm = new Murmur3();
            rootObject.results.First().events.First().HashedByteValue =
                rootObject.results.First().events.First().GenerateHashedByteValue(hashingAlgorithm);
            var result = _rootObjectRepository.SaveRootObject(rootObject);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Id > 0);
        }

        [Test]
        public void rootObject_should_be_retrieved_from_database()
        {
            var rootObjectId = 1;
            var hashedValue = _rootObjectRepository.GetRootObject(rootObjectId);

            Assert.IsNotNull(hashedValue);
            Assert.IsTrue(hashedValue.Id > 0);
        }

        private static string GenerateRandomHashValue()
        {
            return "testing" + new Random(13 * ((int)DateTime.Now.Ticks))
                .Next(Int32.MinValue, Int32.MaxValue);
        }
    }
}
