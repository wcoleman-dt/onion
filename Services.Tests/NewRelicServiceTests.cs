using System;
using System.Diagnostics;
using System.Linq;
using Domain.HashingAlgorithm;
using Domain.Interfaces;
using Domain.Utility;
using Infrastructure.Data;
using NUnit.Framework;
using Services.NewRelic;

namespace Services.Tests
{
    [TestFixture]
    public class NewRelicServiceTests
    {
        [Test]
        public void GetTransactionsSinceLastInterval_should_return_transactions()
        {
            try
            {
                const int RESULTLIMIT = 1000;  

                IApiEndPointRepository apiConfigurator = new ApiEndPointRepository();
                IHashingAlgorithm hashingAlgorithm = new Murmur3();
                var newRelicService = new NewRelicService(apiConfigurator);
                var rootObject = newRelicService.GetTransactionsSinceLastInterval(
                    new TimeSpan(1, 0, 0, 0), RESULTLIMIT);

                Assert.IsNotNull(rootObject);
                Assert.IsTrue(rootObject?.results?.Any());

                if (rootObject?.results != null)
                {
                    foreach (var result in rootObject?.results)
                    {
                        if (result?.events != null)
                        {
                            foreach (var eventObj in result?.events)
                            {
                                eventObj.HashedByteValue = eventObj.GenerateHashedByteValue(hashingAlgorithm);
                            }
                        }
                    }
                }
                RootObjectRepository rootObjectRepository = new RootObjectRepository();
                rootObjectRepository.SaveRootObject(rootObject);

                Assert.IsNotNull(rootObject);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }
    }
}
