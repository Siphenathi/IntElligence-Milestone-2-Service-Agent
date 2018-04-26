using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TaskExecutor.Boundary;

namespace TaskExecutor.MachineInformation.TaskExecutorLibrary
{
    public class IpAddress:IIpAddress
    {
        protected readonly IComputerName ComputerName;

        public IpAddress()
        {
            ComputerName = new ComputerName();
        }
        public int RunAndReturnIP()
        {
            Console.WriteLine(GetIpAddress());
            return 0;
        }
        public string GetIpAddress()
        {
            return Dns.GetHostByName(ComputerName.GetComputerName()).AddressList[0].ToString();
        }
    }
}
