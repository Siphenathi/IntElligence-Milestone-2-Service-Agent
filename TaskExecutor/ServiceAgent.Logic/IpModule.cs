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
            ExecuteIpAddressEndPoint(ipAddress);
        }

        private void ExecuteIpAddressEndPoint(IIpAddress ipAddress)
        {
            Get["/api/ip"] = parameters =>
            {
                var model = new IpModel { IpAddress = ipAddress.GetIpAddress() };
                return Negotiate.WithStatusCode(HttpStatusCode.OK)
                    .WithModel(model);

            };
        }
    }
}
