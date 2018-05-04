using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.Responses.Negotiation;
using Nancy.Testing;
using Newtonsoft.Json;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
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
    public class ScriptTests
    {

        [Test]
        public void GetScript_GivenValidScriptCommand_ShouldExecuteValidScriptAndReturnStatusCOde200()
        {
            // Arrange
            var environment = Substitute.For<IPowershellScript>();
            var command = $"Write-Output \"Hello world\"";

            environment.GetScriptOutput(command).Returns("Hello world");
            var browser = CreateBrowser(environment);

            var scriptModel = new ScriptModel {Script = command};

            var expected = "Hello world";
            // Act
            var result = browser.Post("/api/script", with =>
            {
                with.HttpRequest();
                with.Header("Accept", "application/json");
                with.JsonBody(scriptModel);

            });
            var actual = JsonConvert.DeserializeObject<ScriptModel>(result.Body.AsString());

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(expected, actual.Script);

        }

        [TestCase("")]
        [TestCase(null)]
        public void GetScript_GivenEmptyCommand_ShouldReturnStatusCOde400(string command)
        {
            // Arrange
            var environment = Substitute.For<IPowershellScript>();

            environment.GetScriptOutput(command).Returns("");
            var browser = CreateBrowser(environment);

            var scriptModel = new ScriptModel { Script = command };

            // Act
            var result = browser.Post("/api/script", with =>
            {
                with.HttpRequest();
                with.Header("Accept", "application/json");
                with.JsonBody(scriptModel);

            });

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);

        }

        [Test]
        public void GetScript_GivenInvalidScriptCommand_ShouldReturnStatusCode400AndPowershellErrorMessage()
        {
            // Arrange
            var environment = Substitute.For<IPowershellScript>();
            var command = $"hello";

            environment.GetScriptOutput(command).Returns("The term \'hello\' is not recognized as the name of a cmdlet, function, script file, or operable program. Check the spelling of the name, or if a path was included, verify that the path is correct and try again.");
            var browser = CreateBrowser(environment);

            var scriptModel = new ScriptModel { Script = command };

            var expected = "The term \'hello\' is not recognized as the name of a cmdlet, function, script file, or operable program. Check the spelling of the name, or if a path was included, verify that the path is correct and try again.";
            // Act
            var result = browser.Post("/api/script", with =>
            {
                with.HttpRequest();
                with.Header("Accept", "application/json");
                with.JsonBody(scriptModel);

            });
            var actual = JsonConvert.DeserializeObject<ScriptModel>(result.Body.AsString());

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.AreEqual(expected, actual.Script);

        }

        [Test]
        public void GivenFailureOfIpEndpoint_ShouldReturnStatusCode500()
        {
            // Arrange

            var environment = Substitute.For<IPowershellScript>();
            var command = "hello";
            environment.GetScriptOutput("hello").Throws(new Exception("Failed to execute endpoint"));

            var bootstrapper = CreateCustomBootstrapper(environment);

            var browser = new Browser(bootstrapper);

            var expected = "Failed to execute endpoint";

            var scriptModel = new ScriptModel { Script = command };
            // Act
            var result = browser.Post("/api/script", with =>
            {
                with.HttpRequest();
                with.Header("Accept", "application/json");
                with.JsonBody(scriptModel);

            });
            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);
            Assert.AreEqual(expected, result.Body.AsString());
        }



        public static CustomBootstrapper CreateCustomBootstrapper(IPowershellScript environment)
        {
            var bootstrapper = new CustomBootstrapper(with =>
            {
                with.Module<ScriptModule>();
                with.Dependency(environment);
            });
            return bootstrapper;
        }

        public static Browser CreateBrowser(IPowershellScript environment)
        {
            var browser = new Browser(with =>
            {
                with.Module<ScriptModule>();
                with.Dependency(environment);
            });
            return browser;
        }
    }
}
