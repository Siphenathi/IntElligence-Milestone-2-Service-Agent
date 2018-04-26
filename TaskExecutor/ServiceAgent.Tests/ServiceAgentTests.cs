using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.Testing;
using Newtonsoft.Json;
using NUnit.Framework;
using ServiceAgent.Model;
using TaskExecutor.Boundary;
using TaskExecutor.MachineInformation;

namespace ServiceAgent.Tests
{
    [TestFixture]
    public class ServiceAgentTests
    {
        [Test]
        public void CheckHealth_GivenHealthEndpoint_ShouldReturnStatusCode200()
        {
            // Arrange
            var bootstrapper = new DefaultNancyBootstrapper();
            var browser = new Browser(bootstrapper,  to => to.Accept("application/json"));

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
            var bootstrapper = new DefaultNancyBootstrapper();
            var browser = new Browser(bootstrapper, to => to.Accept("application/json"));
            
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
            var bootstrapper = new DefaultNancyBootstrapper();
            var browser = new Browser(bootstrapper, to => to.Accept("application/json"));
            var environment = CreatEnvironmentHandler();
            var hostnameModel = new HostNameModel { hostName = environment.GetHostName() };
            var expected = JsonConvert.SerializeObject(hostnameModel);

            //JsonConvert.DeserializeObject<>(
            // Act
            var result = browser.Get("/hostname", with =>
            {
                with.HttpRequest();
            });

            // Assert
            Assert.AreEqual(expected, result.Body.AsString());
        }

        public IEnvironmentHandler CreatEnvironmentHandler()
        {
            return new EnvironmentHandler();
        }
    }
}
