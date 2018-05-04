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
            ExecuteScriptEndPoint(powershellScript);
        }

        public void ExecuteScriptEndPoint(IPowershellScript powershellScript)
        {
            Post["/api/script"] = parameters =>
            {

                var scriptContent = (this).Bind<ScriptModel>();

                if (IsNotValid(scriptContent))
                    return Negotiate.WithStatusCode(HttpStatusCode.BadRequest);

                var command = scriptContent.Script;

                scriptContent.Script = powershellScript.GetScriptOutput(command);

                if (IsNotRecognized(scriptContent))
                {
                    return Negotiate.WithStatusCode(HttpStatusCode.BadRequest)
                        .WithModel(scriptContent);
                }

                return Negotiate.WithStatusCode(HttpStatusCode.OK)
                    .WithModel(scriptContent);

            };
        }

        private static bool IsNotRecognized(ScriptModel scriptContent)
        {
            return scriptContent.Script.Contains("is not recognized");
        }

        private static bool IsNotValid(ScriptModel scriptContent)
        {
            return string.IsNullOrEmpty(scriptContent.Script);
        }
    }
}
