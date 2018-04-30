using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TaskExecutor.Boundary;

namespace TaskExecutor.MachineInformation.TaskExecutorLibrary
{
    public class IpAddress :IIpAddress
    {
        public int RunAndReturnIP()
        {
            Console.WriteLine(GetIpAddress());
            return 0;
        }
        public string GetIpAddress()
        {
            return Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();
        }
    }
}
