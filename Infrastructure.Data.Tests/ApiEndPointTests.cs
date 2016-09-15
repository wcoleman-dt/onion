using Domain.Entities;
using Domain.Interfaces;
using NUnit.Framework;

namespace Infrastructure.Data.Tests
{
    [TestFixture]
    public class ApiEndPointTests
    {
        readonly IApiEndPointRepository _apiEndPointRepository = new ApiEndPointRepository();

        [Test]
        public void apiEndpoint_should_save_to_database()
        {
            ApiEndpoint apiEndpoint = new ApiEndpoint()
            {
                AccountId=1,
                ApiKey = "Duiwoelskj3342ksd",
                ApiKeyType = 2,
                Curl = "testing",
                Endpoint = "https://www.google.com",
                NRSQLSyntax = "Select * from Transaction",
                Title = "Test Api Endpoint"
            };

            var result = _apiEndPointRepository.SaveEndPoint(apiEndpoint);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Id > 0);
        }

        [Test]
        public void rootObject_should_be_retrieved_from_database()
        {
            var apiKeyType = 1;
            var apiEndPoint = _apiEndPointRepository.GetEndPoint(apiKeyType);

            Assert.IsNotNull(apiEndPoint);
            Assert.IsTrue(apiEndPoint.Id > 0);
        }

    }
}
