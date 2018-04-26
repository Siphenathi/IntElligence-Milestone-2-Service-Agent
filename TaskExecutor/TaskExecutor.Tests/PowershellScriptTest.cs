using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TaskExecutor.Boundary;
using TaskExecutor.MachineInformation.TaskExecutorLibrary;

namespace TaskExecutor.Tests
{
    [TestFixture]
    public class PowershellScriptTest
    {
        [Test]
        public void GetScriptOutput_GivenScriptCommand_ShouldReturnScriptOutput()
        {
            //Arrange
            var powershellScript = CreatePowershellScript();
            var path = string.Empty;
            var myDirectory = new DirectoryInfo(TestContext.CurrentContext.TestDirectory).Parent.Parent.Parent;
            path = $"{myDirectory.FullName}\\TaskExecutor.MachineInformation\\bin\\Debug\\TaskExecutorScript\\HelloWorld.ps1";

            //Act
            var actual = powershellScript.GetScriptOutput(path);

            //Assert
            var expected = "Hello world";
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void GetScriptOutput_GivenInvalidScriptCommand_ShouldReturnScriptOutput()
        {
            //Arrange
            var powershellScript = CreatePowershellScript();

            var path = string.Empty;
            var myDirectory = new DirectoryInfo(TestContext.CurrentContext.TestDirectory).Parent.Parent.Parent;
            path = $"{myDirectory.FullName}\\TaskExecutor.MachineInformation\\bin\\Debug\\TaskExecutorScript\\Empty.ps1";

            //Act
            var actual = powershellScript.GetScriptOutput(path);

            //Assert
            var expected = "The term \'hello\' is not recognized as the name of a cmdlet, function, script file, or operable program. " +
                           "Check the spelling of the name, or if a path was included, verify that the path is correct and try again.";
            Assert.AreEqual(expected, actual);
        }

        private IPowershellScript CreatePowershellScript()
        {
            return new PowershellScript();
        }
    }
}
