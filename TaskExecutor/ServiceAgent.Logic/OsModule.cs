using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using ServiceAgent.Model;
using TaskExecutor.Boundary;

namespace ServiceAgent.Logic
{
    public class OsModule : NancyModule
    {
        public OsModule(IOperatingSystems operatingSystem)
        {
            ExecuteOsEndPoint(operatingSystem);
        }

        private void ExecuteOsEndPoint(IOperatingSystems operatingSystem)
        {
            Get["/api/os"] = parameters =>
            {
                var model = new OsModel { osVersion = operatingSystem.GetOperatingSystem() };
                return Negotiate.WithStatusCode(HttpStatusCode.OK)
                    .WithModel(model);
            };
        }
    }
}
