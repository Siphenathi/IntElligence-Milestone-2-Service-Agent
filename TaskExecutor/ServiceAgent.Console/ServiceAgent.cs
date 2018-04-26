using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;

namespace ServiceAgent
{
    public class ServiceAgent : NancyModule
    {
        public ServiceAgent()
        {
            CheckHealth();
        }

        public void CheckHealth()
        {
            Get["/Health"] = parameters =>
            {
                return Negotiate.WithStatusCode(HttpStatusCode.OK);
            };
        }
    }
}
