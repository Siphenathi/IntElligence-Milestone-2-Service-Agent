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
using TaskExecutor.MachineInformation.TaskExecutorLibrary;

namespace ServiceAgent.Tests
{
    public class HostNameTest
    {
        [Test]
        public void GetHostName_GivenHostNameEndpoint_ShouldReturnHostnameAndStatusCode200()
        {
            // Arrange
            var environment = Substitute.For<IComputerName>();
            var fullyQualified = Substitute.For<IFullyQualifiedComputerName>();
            environment.GetComputerName().Returns("DEVFLUENCE6-DBN");

            var browser = CreateBrowser(environment, fullyQualified);

            var expectedHostName = "DEVFLUENCE6-DBN";

            // Act
            var result = browser.Get("/api/hostname", with =>
            {
                with.HttpRequest();
                with.Header("Accept", "application/json");
            });

            var actual = JsonConvert.DeserializeObject<HostNameModel>(result.Body.AsString());
            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(expectedHostName, actual.HostName);

        }

        [Test]
        public void GetHostName_GivenFullyQualifiedHostNameEndpoint_ShouldReturnFullyQualifiedHostnameAndStatusCode200()
        {
            // Arrange
            var environment = Substitute.For<IComputerName>();
            var fullyQualified = Substitute.For<IFullyQualifiedComputerName>();
            fullyQualified.GetFullyQualifiedComputerName().Returns("DEVFLUENCE6-DBN");

            var browser = CreateBrowser(environment, fullyQualified);

            var expectedHostName = "DEVFLUENCE6-DBN";

            // Act
            var result = browser.Get("/api/hostname", with =>
            {
                with.HttpRequest();
                with.Header("Accept", "application/json");
                with.Query("fully-Qualified", "true");
            });

            var actual = JsonConvert.DeserializeObject<HostNameModel>(result.Body.AsString());
            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(expectedHostName, actual.HostName);
            fullyQualified.Received().GetFullyQualifiedComputerName();

        }

        [Test]
        public void GivenFailureOfHostnameEndpoint_ShouldReturnStatusCode500()
        {
            // Arrange

            var environment = Substitute.For<IComputerName>();
            var fullyQualifiedComputerName = Substitute.For<IFullyQualifiedComputerName>();
            environment.GetComputerName().Throws(new Exception("Failed to execute endpoint"));

            var bootstrapper = CreateCustomBootstrapper(environment, fullyQualifiedComputerName);

            var browser = new Browser(bootstrapper);

            var expected = "Failed to execute endpoint";

            // Act
            var result = browser.Get("/api/hostname", with =>
            {
                with.HttpRequest();
            });
            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);
            Assert.AreEqual(expected, result.Body.AsString());
        }

        [Test]
        public void GivenFailureOfFullyQualifiedHostnameEndpoint_ShouldReturnStatusCode500()
        {
            // Arrange

            var environment = Substitute.For<IComputerName>();
            var fullyQualifiedComputerName = Substitute.For<IFullyQualifiedComputerName>();
            fullyQualifiedComputerName.GetFullyQualifiedComputerName().Throws(new Exception("Failed to execute endpoint"));

            var bootstrapper = CreateCustomBootstrapper(environment, fullyQualifiedComputerName);

            var browser = new Browser(bootstrapper);

            var expected = "Failed to execute endpoint";

            // Act
            var result = browser.Get("/api/hostname", with =>
            {
                with.HttpRequest();
                with.Query("fully-Qualified", "true");
            });
            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);
            Assert.AreEqual(expected, result.Body.AsString());
        }


        public static CustomBootstrapper CreateCustomBootstrapper(IComputerName environment, IFullyQualifiedComputerName fullyQualifiedComputerName)
        {
            var bootstrapper = new CustomBootstrapper(with =>
            {
                with.Module<HostnameModule>();
                with.Dependency(environment);
                with.Dependency(fullyQualifiedComputerName);
            });
            return bootstrapper;
        }

        public static Browser CreateBrowser(IComputerName environment, IFullyQualifiedComputerName fullyQualified)
        {
            var browser = new Browser(with =>
            {

                with.Module<HostnameModule>();
                with.Dependency(environment);
                with.Dependency(fullyQualified);
            });
            return browser;
        }
    }
}
