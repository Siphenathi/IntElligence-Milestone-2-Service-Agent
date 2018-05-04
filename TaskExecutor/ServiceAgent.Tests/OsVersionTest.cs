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
    public class OsVersionTest
    {
        [Test]
        public void GetOs_GivenOsEndpoint_ShouldReturnOsAndStatusCode200()
        {
            // Arrange
            var environment = Substitute.For<IOperatingSystems>();
            environment.GetOperatingSystem().Returns("Microsoft Windows NT 10.0.16299.0");
            var browser = new Browser(with =>
            {

                with.Module<OsModule>();
                with.Dependency<IOperatingSystems>(environment);
            });

            var expectedOs = "Microsoft Windows NT 10.0.16299.0";

            // Act
            var result = browser.Get("/api/os", with =>
            {
                with.HttpRequest();
                with.Header("Accept", "application/json");

            });
            var actual = JsonConvert.DeserializeObject<OsModel>(result.Body.AsString());

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(expectedOs, actual.osVersion);

        }

        [Test]
        public void GivenFailureOfIpEndpoint_ShouldReturnStatusCode500()
        {
            // Arrange

            var environment = Substitute.For<IOperatingSystems>();
            environment.GetOperatingSystem().Throws(new Exception("Failed to execute endpoint"));

            var bootstrapper = CreateCustomBootstrapper(environment);

            var browser = new Browser(bootstrapper);

            var expected = "Failed to execute endpoint";

            // Act
            var result = browser.Get("/api/os", with =>
            {
                with.HttpRequest();
            });
            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);
            Assert.AreEqual(expected, result.Body.AsString());
        }

        public static CustomBootstrapper CreateCustomBootstrapper(IOperatingSystems environment)
        {
            var bootstrapper = new CustomBootstrapper(with =>
            {
                with.Module<OsModule>();
                with.Dependency(environment);
            });
            return bootstrapper;
        }

        public static Browser CreateBrowser(IOperatingSystems environment)
        {
            var browser = new Browser(with =>
            {
                with.Module<OsModule>();
                with.Dependency(environment);
            });
            return browser;
        }
    }
}
