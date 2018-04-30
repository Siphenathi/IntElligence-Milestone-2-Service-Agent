using System;
using System.Net;
using TaskExecutor.Boundary;

namespace TaskExecutor.MachineInformation.TaskExecutorLibrary
{
    public class ComputerName :IComputerName
    {

        public int RunAndReturnPcName()
        {
            Console.WriteLine(GetComputerName());
            return 0;
        }
        public string GetComputerName()
        {
            return Environment.MachineName;
        }

       
    }
}
