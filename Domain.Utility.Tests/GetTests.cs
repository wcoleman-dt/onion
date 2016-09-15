using NUnit.Framework;

namespace Domain.Utility.Tests
{
    [TestFixture]
    public class GetTests
    {
        [Test]
        public void httpget_should_return_a_result()
        {
            var url = "http://www.google.com";
            Get.BaseUrl = url;
            var httpGet = Get.HttpGet("?q=test");
            var response = httpGet.Content.ReadAsStringAsync();
            Assert.IsTrue(!string.IsNullOrEmpty(response.Result));
        }

    }
}
