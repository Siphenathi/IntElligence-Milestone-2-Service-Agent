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
    public class IpAddressTest
    {

        [Test]
        public void GetIPAddressName_GivenIPCommand_ShouldReturnIPAddress()
        {
            //Arrange
            IpAddress result = CreateMachineInformation();
            var environmentHandler = CreatEnvironmentHandler();
            //Act
            var actual = result.GetIpAddress();
            
            //Assert
            var expected = environmentHandler.GetIpAddress();
            Assert.AreEqual(expected, actual);
        }
        private static IpAddress CreateMachineInformation()
        {
            return new IpAddress();
        }

        public IEnvironmentHandler CreatEnvironmentHandler()
        {
            return new EnvironmentHandler();
        }
    }
}
