using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TaskExecutor.Boundary;
using TaskExecutor.MachineInformation;
using TaskExecutor.MachineInformation.TaskExecutorLibrary;

namespace TaskExecutor.Tests
{
    [TestFixture]
    public class OperatingSystemTest
    {
        [Test]
        public void GetOperatingSystem_GivenOsCommand_ShouldReturnComputerOperatingSystem()
        {
            //Arrange
            var os = CreateOperatingSystem();
            var environment = CreateEnvironment();
            //Act
            var actual = os.GetOperatingSystem();
            //Assert
            var expected = environment.GetOsVersion();
            Assert.AreEqual(expected, actual);
        }

        public IOperatingSystem CreateOperatingSystem()
        {
            return new OperatingSystems();
        }

        public IEnvironmentHandler CreateEnvironment()
        {
            return new EnvironmentHandler();
        }
    }
}
