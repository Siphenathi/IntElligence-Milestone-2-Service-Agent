using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TaskExecutor.Boundary;

namespace TaskExecutor.MachineInformation.TaskExecutorLibrary
{
    public class FullyQualifiedComputerName:IFullyQualifiedComputerName
    {
        private readonly ComputerName _computer = new ComputerName();

        public string GetFullyQualifiedComputerName()
        {
            var hostname = _computer.GetComputerName();
            var hostEntry = Dns.GetHostEntry(hostname);
            return hostEntry.HostName;
        }

    }
}
