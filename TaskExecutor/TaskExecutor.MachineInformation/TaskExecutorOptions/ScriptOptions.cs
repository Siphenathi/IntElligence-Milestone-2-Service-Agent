using CommandLine;

namespace TaskExecutor.MachineInformation.TaskExecutorOptions
{
    [Verb("script", HelpText = "Get script output ")]
    public class ScriptOptions
    {
        [Option('f', "file", HelpText = "Get powershell script output")]
        public string PowershellScriptOutput { get; set; }
    }
}
