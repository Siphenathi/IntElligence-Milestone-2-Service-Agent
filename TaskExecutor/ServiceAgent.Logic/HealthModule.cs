using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;

namespace ServiceAgent.Logic
{
    public class HealthModule : NancyModule
    {
        public HealthModule()
        {
            ExecuteHealthEndPoint();
        }

        private void ExecuteHealthEndPoint()
        {
            Get["/api/health"] = p => HttpStatusCode.OK;
        }
    }
}
