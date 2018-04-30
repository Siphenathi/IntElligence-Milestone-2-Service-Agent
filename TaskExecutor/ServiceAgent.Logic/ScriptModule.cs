using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.ModelBinding;
using ServiceAgent.Model;
using TaskExecutor.Boundary;

namespace ServiceAgent.Logic
{
    public class ScriptModule : NancyModule
    {
        public ScriptModule(IPowershellScript powershellScript)
        {
            Post["/script"] = parameters =>
            {

                var scriptContent = this.Bind<ScriptModel>();
                var command = scriptContent.Script;

                    scriptContent.Script = powershellScript.GetScriptOutput(command);

                return Negotiate.WithStatusCode(HttpStatusCode.OK)
                    .WithModel(scriptContent);
                ;
            };
        }
    }
}
