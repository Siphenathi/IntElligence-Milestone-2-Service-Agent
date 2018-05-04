using Nancy;
using Nancy.Testing;
using NUnit.Framework;
using ServiceAgent.Logic;

namespace ServiceAgent.Tests
{
    public class HealthTest
    {
        [Test]
        public void CheckHealth_GivenHealthEndpoint_ShouldReturnStatusCode200()
        {
            // Arrange
            var browser = new Browser(with => with.Module<HealthModule>());

            // Act
            var result = browser.Get("/api/health", with =>
            {
                with.HttpRequest();
            });

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }
    }
}