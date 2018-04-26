using CommandLine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Net;
using System.Net.NetworkInformation;
using TaskExecutor.MachineInformation;

namespace MachineInformation
{
    public class ExecuteTask
    {
        string message;
        int code;

        public void RunOptions(string[] args)
        {
            Parser.Default.ParseArguments<OSOptions, PCOptions, IPOptions, ScriptOptions>(args)
              .MapResult(
                (OSOptions opts) => RunAndReturnOS(),
                (PCOptions opts) => RunAndReturnPcName(),
                (IPOptions opts) => RunAndReturnIP(),
                (ScriptOptions opts) => RunAndReturnScriptOutput(),
                AdjustErrorLevelWhenHelpRequested);
         }

        private int AdjustErrorLevelWhenHelpRequested(IEnumerable<Error> errs)
        {
            return errs.First().Tag == ErrorType.HelpVerbRequestedError ? 0 : 1;

        }

        private int RunAndReturnOS()
        {
            Console.WriteLine(GetOperatingSystem());
            return 0;
        }

        private int RunAndReturnPcName()
        {
            Console.WriteLine(GetComputerName());
            return 0;
        }

        private int RunAndReturnIP()
        {
            Console.WriteLine(GetIPAddress());
            return 0;
        }
        private int RunAndReturnScriptOutput()
        {
            
            Console.WriteLine(GetScriptOutput());
            return Environment.ExitCode;
        }
        public string GetComputerName()
        {
            return Environment.MachineName;
        }
        public string GetIPAddress()
        {
            return Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();
        }

        public string GetOperatingSystem()
        {
            return Environment.OSVersion.ToString();
        }

        public string GetFullyQualifiedComputerName()
        {
            var hostname = GetComputerName();
            var hostEntry = Dns.GetHostEntry(hostname);
            return hostEntry.HostName;
        }
  
        public string GetScriptOutput()
        {
            //var text = System.IO.File.ReadAllText(@"C:\git\TeamGriffins-Master\TaskExecutor\TaskExecutor.Tests\Empty.ps1");

            //using (PowerShell PowerShellInstance = PowerShell.Create())
            //{
            //    PowerShellInstance.AddScript(text);
            //    Collection<PSObject> PSOutput = PowerShellInstance.Invoke();

            //    foreach (PSObject outputItem in PSOutput)
            //    {
            //        if (outputItem != null) return outputItem.BaseObject.ToString();
            //    }
            //    if (PowerShellInstance.Streams.Error.Count > 0)
            //    {

            //    }
            //    return text;

            //}

            var text = System.IO.File.ReadAllText(@"C:\git\TeamGriffins-Master\TaskExecutor\TaskExecutor.Tests\HelloWorld.ps1");
            var scriptResult = string.Empty;
            using (PowerShell pshell = PowerShell.Create())
            {
                pshell.AddScript(text);
                var resultList = pshell.Invoke();

                if (pshell.Streams.Error.Any())
                {
                    Console.WriteLine(GetScriptErrors(pshell));
                }

                foreach (PSObject result in resultList)
                {
                    scriptResult = $"{result}";
                }
            }
            return scriptResult;
        }
        private string GetScriptErrors(PowerShell pshell)
        {
            var scriptResult = string.Empty;
            foreach (var errorRecord in pshell.Streams.Error)
            {
                scriptResult += $"{errorRecord}\n";
            }
            return scriptResult;
        }
        public string GetInvalidScriptOutput()
        {
            var text = System.IO.File.ReadAllText(@"C:\git\TeamGriffins-Master\TaskExecutor\TaskExecutor.Tests\Empty.ps1");     

            using (PowerShell PowerShellInstance = PowerShell.Create())
            {

                PowerShellInstance.AddScript(text);
                Collection<PSObject> PSOutput = PowerShellInstance.Invoke();

                foreach (PSObject outputItem in PSOutput)
                {
                    if (outputItem != null)
                    {
                       return outputItem.BaseObject.ToString();
                       
                    }
                }
                if (PowerShellInstance.Streams.Error.Count > 0)
                {
                     text = PowerShellInstance.Streams.Error.ToString();

                }
                return text;
            }
        }

    }
}
