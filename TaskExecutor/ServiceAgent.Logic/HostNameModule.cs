using Nancy;
using ServiceAgent.Model;
using TaskExecutor.Boundary;
using TaskExecutor.MachineInformation.TaskExecutorLibrary;

namespace ServiceAgent.Logic
{
    public class  HostNameModule:NancyModule
    {
        public HostNameModule(IComputerName computerName)
        {
            Get["/hostname"] = parameters =>
            {
                var model = new HostNameModel { hostName = computerName.GetComputerName() };
                return Negotiate.WithStatusCode(HttpStatusCode.OK)
                                .WithModel(model);
            };
        }
    }
}
