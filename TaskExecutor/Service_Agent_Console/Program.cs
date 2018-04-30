using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Hosting.Self;
using Nancy.Testing;
using ServiceAgent.Logic;
using TaskExecutor.Boundary;
using TaskExecutor.MachineInformation;
using TaskExecutor.MachineInformation.TaskExecutorLibrary;

namespace Service_Agent_Console
{
    public class Program 
    {
        public static void Main(string[] args)
        {
            var hostConfigs = CreateHostConfiguration();
            var bootstrapper = CreateConfigurableBootstrapper();
            var uri = new Uri("http://localhost:1234");

            using (var host = new NancyHost(bootstrapper, hostConfigs, uri))
            {
                host.Start();
                Console.ReadKey();
            }
        }

        private static HostConfiguration CreateHostConfiguration()
        {
            var hostConfigs = new HostConfiguration
            {
                UrlReservations =
                {
                    CreateAutomatically = true
                }
            };
            return hostConfigs;
        }

        private static ConfigurableBootstrapper CreateConfigurableBootstrapper()
        {
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                with.Module<HealthModule>();
                with.Module<HostnameModule>();
                with.Dependency<IComputerName>(typeof(ComputerName));
                with.Module<IpModule>();
                with.Dependency<IIpAddress>(typeof(IpAddress));
                with.Module<OsModule>();
                with.Dependency<IOperatingSystems>(typeof(OperatingSystems));
                with.Module<ScriptModule>();
                with.Dependency<IPowershellScript>(typeof(PowershellScript));
            });
            return bootstrapper;
        }

    }
}

