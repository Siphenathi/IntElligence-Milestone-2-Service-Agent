namespace TaskExecutor.Boundary
{
    public interface ITaskExecutorEnvironmentHandler
    {
        string GetIpAddress();
        string GetHostName();
        string GetFullyQualifiedHostName();
    }
}