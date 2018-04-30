using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
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
            OperatingSystems result = CreateMachineInformation();
            //Act
            var actual = result.GetOperatingSystem();
            //Assert
            var expected = "Microsoft Windows NT 10.0.16299.0";
            Assert.AreEqual(expected, actual);
        }

        private static OperatingSystems CreateMachineInformation()
        {
            return new OperatingSystems();
        }
    }
}
