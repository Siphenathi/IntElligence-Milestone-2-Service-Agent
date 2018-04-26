using CommandLine;

namespace TaskExecutor.MachineInformation.TaskExecutorOptions
{
    [Verb("hostname", HelpText = "Get the computer name ")]
    public class PCOptions
    {
        [Option('f', "fully-qualified", HelpText = "Get the fully qualified computer name")]
        public bool FullyQualifiedHostName { get; set; } 
    }
}
