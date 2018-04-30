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
    public class FullyQualifiedComputerNameTest
    {
        [Test]
        public void GetFullyQualifiedComputerName_GivenHostnameCommandWithFullyQualifiedOptions_ShouldReturnFullyQualifiedComputerName()
        {
            //Arrange
            FullyQualifiedComputerName result = CreateMachineInformation();
            var environmentHandler = CreatEnvironmentHandler();
            //Act
            var actual = result.GetFullyQualifiedComputerName();
            //Assert
            var expected = environmentHandler.GetFullyQualifiedHostName();
            Assert.AreEqual(expected, actual);
        }
        private static FullyQualifiedComputerName CreateMachineInformation()
        {
            return new FullyQualifiedComputerName();
        }

        public ITaskExecutorEnvironmentHandler CreatEnvironmentHandler()
        {
            return new TaskExecutorEnvironmentHandler();
        }
    }

}
