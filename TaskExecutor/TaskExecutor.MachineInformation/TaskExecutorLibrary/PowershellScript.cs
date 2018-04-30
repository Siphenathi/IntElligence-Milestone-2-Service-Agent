using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TaskExecutor.Boundary;
using TaskExecutor.MachineInformation.TaskExecutorOptions;

namespace TaskExecutor.MachineInformation.TaskExecutorLibrary
{
    public class PowershellScript:IPowershellScript
    {
        public int RunAndReturnScriptOutput(ScriptOptions opts)
        {
            var command = ReadFile(opts.PowershellScriptOutput);
            Console.WriteLine(GetScriptOutput(command));
            return Environment.ExitCode;
        }

        public string GetScriptOutput(string command)
        {
            string results = string.Empty;
            using (PowerShell PowerShellInstance = PowerShell.Create())
            {
                PowerShellInstance.AddScript(command);

                Collection<PSObject> PSOutput = PowerShellInstance.Invoke();


                var errorHandler = HasErrors(PowerShellInstance);
                if (errorHandler != null && !errorHandler.Equals(""))
                {
                    return errorHandler;
                }
                results = GetListOfFileContents(results, PSOutput);
            }

            return results;
        }

        public string ReadFile(string path)
        {
            return File.ReadAllText(path);
        }

        private static string HasErrors(PowerShell PowerShellInstance)
        {
            var errorResult = string.Empty;
            Collection<ErrorRecord> errorList = PowerShellInstance.Streams.Error.ReadAll();

            if (errorList != null && errorList.Count > 0)
            {
                foreach (ErrorRecord error in errorList)
                {
                    errorResult += error.Exception.Message;
                }
            }

            return errorResult;
        }

        private static string GetListOfFileContents(string results, Collection<PSObject> PSOutput)
        {
            foreach (PSObject outputItem in PSOutput)
            {
                results = AppendItemsInList(results, outputItem);

            }
            return results;
        }

        private static string AppendItemsInList(string results, PSObject outputItem)
        {
            if (outputItem != null)
            {
                results = outputItem.ToString();

            }

            return results;
        }

    }
}
