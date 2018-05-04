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
        public HostnameModule(IComputerName computerName,IFullyQualifiedComputerName fullyQualifiedHostName)
        {
            ExecuteHostNameEndPoint(computerName, fullyQualifiedHostName);

        }

        private void ExecuteHostNameEndPoint(IComputerName computerName, IFullyQualifiedComputerName fullyQualifiedHostName)
        {
            Get["/api/hostname"] = parameters =>
            {
                var model = new HostNameModel();
                var inputParameter = Request.Query["fully-Qualified"];

                model.HostName = GetHostName(computerName, fullyQualifiedHostName, inputParameter);
                return Negotiate.WithStatusCode(HttpStatusCode.OK)
                    .WithModel(model);
            };
        }

        private static string GetHostName(IComputerName computerName, IFullyQualifiedComputerName fullyQualifiedHostName, dynamic inputParameter)
        {
            bool.TryParse(inputParameter, out bool fullyQualified);
            return fullyQualified
                ? fullyQualifiedHostName.GetFullyQualifiedComputerName()
                : computerName.GetComputerName();
        }
    }
}
