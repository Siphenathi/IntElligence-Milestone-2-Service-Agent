using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Net;
using CommandLine;
using TaskExecutor.Boundary;
using TaskExecutor.MachineInformation.TaskExecutorOptions;

namespace TaskExecutor.MachineInformation.TaskExecutorLibrary
{
    public class ExecuteTask: ITaskExecutor
    {
        private readonly ComputerName _computer =new ComputerName();
        private readonly OperatingSystems _operatingSystem =new OperatingSystems();
        private readonly IpAddress _ipAddress =new IpAddress();
        private readonly PowershellScript _powershellScript =new  PowershellScript();

        public void RunOptions(string[] args)
        {
            Parser.Default.ParseArguments<OSOptions, PCOptions, IPOptions, ScriptOptions>(args)
                .MapResult(
             (OSOptions opts) =>_operatingSystem.RunAndReturnOS(),
            (PCOptions opts) => _computer.RunAndReturnPcName(),
            (IPOptions opts) => _ipAddress.RunAndReturnIP(),
            (ScriptOptions opts) => _powershellScript.RunAndReturnScriptOutput(opts),
            AdjustErrorLevelWhenHelpRequested);
        }

        private int AdjustErrorLevelWhenHelpRequested(IEnumerable<Error> errs)
        {
            return errs.First().Tag == ErrorType.HelpVerbRequestedError ? 0 : 1;
        }

       
        }

}
