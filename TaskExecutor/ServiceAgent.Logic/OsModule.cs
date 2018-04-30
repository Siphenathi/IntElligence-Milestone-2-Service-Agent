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
            Get["/os"] = parameters =>
            {
                var model = new OsModel();
                model.osVersion = operatingSystem.GetOperatingSystem();
                return Negotiate.WithStatusCode(HttpStatusCode.OK)
                    .WithModel(model);
            };
        }
    }
}
