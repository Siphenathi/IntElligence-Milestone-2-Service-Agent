using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TaskExecutor.Boundary;
using TaskExecutor.MachineInformation;
using TaskExecutor.MachineInformation.TaskExecutorLibrary;

namespace TaskExecutor.Tests
{
    [TestFixture]
    public class ComputerNameTest
    {
        [Test]
        public void GetComputerName_GivenHostNameCommand_ShouldReturnComputerName()
        {
            //Arrange
            var computerName = CreateComputerName();
            var environmentHandler = CreatEnvironmentHandler();
            //Act
            var actual = computerName.GetComputerName();
            //Assert
            var expected = environmentHandler.GetHostName();
            Assert.AreEqual(expected, actual);
        }
        private static IComputerName CreateComputerName()
        {
            return new ComputerName();
        }

        public IEnvironmentHandler CreatEnvironmentHandler()
        {
            return new EnvironmentHandler();
        }
    }
   
}


