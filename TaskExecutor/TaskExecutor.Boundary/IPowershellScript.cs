using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace TaskExecutor.Boundary
{
    public interface IPowershellScript
    {
        string GetScriptOutput(string path);
    }
}
