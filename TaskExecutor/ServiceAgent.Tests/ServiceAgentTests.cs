using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.Responses.Negotiation;
using Nancy.Testing;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using ServiceAgent.Logic;
using ServiceAgent.Model;
using TaskExecutor.Boundary;
using TaskExecutor.MachineInformation.TaskExecutorLibrary;
using HttpStatusCode = Nancy.HttpStatusCode;

namespace ServiceAgent.Tests
{
    [TestFixture]
    public class ServiceAgentTests
    {
        [Test]
        public void CheckHealth_GivenHealthEndpoint_ShouldReturnStatusCode200()
        {
            // Arrange
            var browser = new Browser(with => with.Module<HealthModule>());

            // Act
            var result = browser.Get("/health", with =>
            {
                with.HttpRequest();
            });

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [Test]
        public void GetHostName_GivenHostNameEndpoint_ShouldReturnHostnameAndStatusCode200()
        {
            // Arrange
            var environment = Substitute.For<IComputerName>();
            environment.GetComputerName().Returns("DEVFLUENCE6-DBN");

            var browser = new Browser(with =>
            {

                with.Module<HostnameModule>();
                with.Dependency(environment);
            });

            var expectedHostName = "DEVFLUENCE6-DBN";

            // Act
            var result = browser.Get("/hostname", with =>
            {
                with.HttpRequest();
                with.Header("Accept", "application/json");
            });

            var actual = JsonConvert.DeserializeObject<HostNameModel>(result.Body.AsString());
            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(expectedHostName, actual.HostName);


        }

        //[Test]
        //public void GetHostname_GivenFailureOfValidEndpoint_ShouldReturnStatusCode500()
        //{
        //    // Arrange
        //    var environment = Substitute.For<IComputerName>();
        //    environment.GetComputerName();

        //    var browser = new Browser(with =>
        //    {

        //        with.Module<HostnameModule>();
        //        with.Dependency(environment);
        //    });


        //    // Act
        //    var result = browser.Post("/hostname", with =>
        //    {
        //        with.HttpRequest();
        //        with.Header("Accept", "application/json");
        //    });

        //    // Assert
        //    Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);

        //}


        [Test]
        public void GetIpAddress_GivenIpEndpoint_ShouldReturnIpAndStatusCode200()
        {
            // Arrange
            var environment = Substitute.For<IIpAddress>();
            environment.GetIpAddress().Returns("192.168.2.178");
            var browser = new Browser(with =>
            {

                with.Module<IpModule>();
                with.Dependency<IIpAddress>(environment);
            });
            var expectedIpAddress = "192.168.2.178";

            // Act
            var result = browser.Get("/ip", with =>
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
            var result = browser.Get("/os", with =>
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
        public void GetScript_GivenScriptEndpoint_ShouldExecuteValidScriptAndReturnStatusCOde200()
        {
            // Arrange
            var environment = Substitute.For<IPowershellScript>();
            var path = $"TaskExecutor.MachineInformation\\bin\\Debug\\TaskExecutorScript\\HelloWorld.ps1";

            environment.GetScriptOutput(path).Returns("Hello world");
            var browser = new Browser(with =>
            {

                with.Module<ScriptModule>();
                with.Dependency<IPowershellScript>(environment);
            });


            var expected = "Hello world";

            // Act
            var result = browser.Post("/script", with =>
            {
                with.HttpRequest();
                with.Header("Accept", "application/json");

            });
            var actual = JsonConvert.DeserializeObject<ScriptModel>(result.Body.AsString());

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(expected, actual.Script);

        }

        //[Test]
        //public void GetScript_GivenEmptyPowershellScript_ShouldReturnStatusCode400()
        //{
        //    // Arrange
        //    var environment = Substitute.For<IPowershellScript>();
        //    var path = $"TaskExecutor.MachineInformation\\bin\\Debug\\TaskExecutorScript\\Empty.ps1";

        //    environment.GetScriptOutput(path);
        //    var browser = new Browser(with =>
        //    {

        //        with.Module<ScriptModule>();
        //        with.Dependency<IPowershellScript>(environment);
        //    });


        //    // Act
        //    var result = browser.Post("/script", with =>
        //    {
        //        with.HttpRequest();
        //        with.Header("Accept", "application/json");
        //    });

        //    // Assert
        //    Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);

        //}


    }
}
