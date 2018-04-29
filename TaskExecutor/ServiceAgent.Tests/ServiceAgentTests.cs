using Nancy;
using Nancy.Testing;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using ServiceAgent.Model;
using TaskExecutor.Boundary;
using TaskExecutor.MachineInformation.TaskExecutorLibrary;
using ServiceAgent.Logic;

namespace ServiceAgent.Tests
{
    [TestFixture]
    public class ServiceAgentTests
    {
        [Test]
        public void CheckHealth_GivenHealthEndpoint_ShouldReturnStatusCode200()
        {
            // Arrange
            var browser = new Browser(with=>with.Module<HealthModule>());
            // Act
            var result = browser.Get("/health", with =>
            {
                with.HttpRequest();
            });

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [Test]
        public void GetHostName_GivenHostNameEndpoint_ShouldReturnStatusCode200()
        {
            // Arrange
            var environment = Substitute.For<ComputerName>();
            var browser = new Browser(with =>
            {
                with.Module<HostNameModule>();
                with.Dependencies<IComputerName>(environment);
            });

            // Act
            var result = browser.Get("/hostname", with =>
            {
                with.HttpRequest();
            });

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [Test]
        public void GetHostName_GivenHostNameEndpoint_ShouldReturnHostName()
        {
            // Arrange
            var environment = Substitute.For<ComputerName>();

            var browser = new Browser(with =>
            {
                with.Module<HostNameModule>();
                with.Dependencies<IComputerName>(environment);
            });

            // Act
            var result = browser.Get("/hostname", with =>
            {
                with.HttpRequest();
            });

            environment.Received().GetComputerName();
        }
    }
}
