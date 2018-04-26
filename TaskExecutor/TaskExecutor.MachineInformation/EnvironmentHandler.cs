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
using TaskExecutor.Boundary;

namespace TaskExecutor.MachineInformation
{
    public class EnvironmentHandler:IEnvironmentHandler
    {

        public string GetIpAddress()
        {
            var ipAddress = string.Empty;
            var localIPs = Dns.GetHostAddresses(GetHostName());
            foreach (var addr in localIPs)
            {
                if (addr.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddress = addr.ToString();
                }
            }
            return ipAddress;
        }

        public string GetHostName()
        {
            return Environment.MachineName;
        }

        public string GetFullyQualifiedHostName()
        {
            var hostname = GetHostName();
            var hostEntry = Dns.GetHostEntry(hostname);
            return hostEntry.HostName;
        }

        public string GetOsVersion()
        {
            return Environment.OSVersion.ToString();
        }
    }
}
