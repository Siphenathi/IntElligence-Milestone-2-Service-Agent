using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskExecutor.Boundary;

namespace TaskExecutor.MachineInformation.TaskExecutorLibrary
{
    public class OperatingSystems:IOperatingSystems
    {
        public int RunAndReturnOS()
        {
            Console.WriteLine(GetOperatingSystem());
            return 0;
        }
        public string GetOperatingSystem()
        {
            return Environment.OSVersion.ToString();
        }
    }
}
