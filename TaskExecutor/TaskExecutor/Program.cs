using System;
using TaskExecutor.MachineInformation;
using TaskExecutor.MachineInformation.TaskExecutorLibrary;

namespace TaskExecutor
{
    public class Program
    {
        static void Main(string[] args)
        {
            ExecuteTask info = new ExecuteTask();
            info.RunOptions(args);
         }
    }
}
