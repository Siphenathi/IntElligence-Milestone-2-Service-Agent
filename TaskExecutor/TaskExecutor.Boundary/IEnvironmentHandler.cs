namespace TaskExecutor.Boundary
{
    public interface IEnvironmentHandler
    {
        string GetIpAddress();
        string GetHostName();
        string GetFullyQualifiedHostName();
        string GetOsVersion();
    }
}