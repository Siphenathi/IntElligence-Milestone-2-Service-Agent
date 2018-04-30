using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using ServiceAgent.Model;
using TaskExecutor.Boundary;
using TaskExecutor.MachineInformation.TaskExecutorLibrary;

namespace ServiceAgent.Logic
{
    public class HostnameModule : NancyModule
    {
        public HostnameModule(IComputerName computerName)
        {
            Get["/hostname"] = parameters =>
            {
                try
                {
                    var model = new HostNameModel();
                    model.HostName = computerName.GetComputerName();
                    return Negotiate.WithStatusCode(HttpStatusCode.OK)
                        .WithModel(model);
                }
                catch (Exception ex)
                {
                    return Negotiate.WithStatusCode(HttpStatusCode.InternalServerError);
                }
            };
        }
    }
}
