using Nancy;
using Nancy.Testing;
using NUnit.Framework;
using ServiceAgent.Logic;

namespace ServiceAgent.Tests
{
    public class InvalidEndpointTest
    {
        [TestCase("/api/")]
        [TestCase("/api/something")]
        [TestCase("/api/"+null)]
        public void GivenInvalidEndpoint_ShouldReturnStatusCode404(string endpoint)
        {
            // Arrange
            var browser = new Browser(with => with.Module<HealthModule>());
            // Act
            var result = browser.Get($"{endpoint}", with =>
            {
                with.HttpRequest();
            });

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}