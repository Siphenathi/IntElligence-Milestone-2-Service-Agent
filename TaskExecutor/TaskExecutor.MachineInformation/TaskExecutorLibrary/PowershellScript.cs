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
            
            Console.WriteLine(GetScriptOutput(opts.PowershellScriptOutput));
            return Environment.ExitCode;
        }

        public string GetScriptOutput(string path)
        {
            var results = string.Empty;
            using (var powerShellInstance = PowerShell.Create())
            {
                powerShellInstance.AddScript(File.ReadAllText(path));

                var psOutput = powerShellInstance.Invoke();


                var errorHandler = HasErrors(powerShellInstance);
                if (errorHandler != null && !errorHandler.Equals(""))
                {
                    return errorHandler;
                }
                results = GetListOfFileContents(results, psOutput);
            }

            return results;
        }


        private static string HasErrors(PowerShell powerShellInstance)
        {
            var errorResult = string.Empty;
            var errorList = powerShellInstance.Streams.Error.ReadAll();

            if (errorList == null || errorList.Count <= 0) return errorResult;
            foreach (var error in errorList)
            {
                errorResult += error.Exception.Message;
            }

            return errorResult;
        }

        private static string GetListOfFileContents(string results, IEnumerable<PSObject> psOutput)
        {
            foreach (var outputItem in psOutput)
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
