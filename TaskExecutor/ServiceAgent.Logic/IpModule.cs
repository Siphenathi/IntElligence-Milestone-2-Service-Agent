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
    public class IpModule : NancyModule
    {
        public IpModule(IIpAddress ipAddress)
        {
            Get["/ip"] = parameters =>
            {
                var model = new IpModel();
                model.IpAddress = ipAddress.GetIpAddress();
                return Negotiate.WithStatusCode(HttpStatusCode.OK)
                    .WithModel(model);

            };
        }
    }
}
