using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.Testing;
using Newtonsoft.Json;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using ServiceAgent.Logic;
using ServiceAgent.Model;
using TaskExecutor.Boundary;

namespace ServiceAgent.Tests
{
    public class IpAddressTest
    {
        [Test]
        public void GetIpAddress_GivenIpEndpoint_ShouldReturnIpAndStatusCode200()
        {
            // Arrange
            var environment = Substitute.For<IIpAddress>();
            environment.GetIpAddress().Returns("192.168.2.178");
            var browser = CreateBrowser(environment);

            var expectedIpAddress = "192.168.2.178";

            // Act
            var result = browser.Get("/api/ip", with =>
            {
                with.HttpRequest();
                with.Header("Accept", "application/json");
            });

            var actual = JsonConvert.DeserializeObject<IpModel>(result.Body.AsString());

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(expectedIpAddress, actual.IpAddress);

        }
        [Test]
        public void GivenFailureOfIpEndpoint_ShouldReturnStatusCode500()
        {
            // Arrange

            var environment = Substitute.For<IIpAddress>();
            environment.GetIpAddress().Throws(new Exception("Failed to execute endpoint"));

            var bootstrapper = CreateCustomBootstrapper(environment);

            var browser = new Browser(bootstrapper);

            var expected = "Failed to execute endpoint";

            // Act
            var result = browser.Get("/api/ip", with =>
            {
                with.HttpRequest();
            });
            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);
            Assert.AreEqual(expected, result.Body.AsString());
        }



        public static CustomBootstrapper CreateCustomBootstrapper(IIpAddress environment)
        {
            var bootstrapper = new CustomBootstrapper(with =>
            {
                with.Module<IpModule>();
                with.Dependency(environment);
            });
            return bootstrapper;
        }

        public static Browser CreateBrowser(IIpAddress environment)
        {
            var browser = new Browser(with =>
            {

                with.Module<IpModule>();
                with.Dependency(environment);
            });
            return browser;
        }
    }
}
