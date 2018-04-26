using System;
using Nancy;
using Newtonsoft.Json;
using ServiceAgent.Model;
using TaskExecutor.Boundary;
using TaskExecutor.MachineInformation.TaskExecutorLibrary;


namespace ServiceAgent.Logic
{
    public class ServiceAgent : NancyModule
    {
        protected readonly IComputerName ComputerName;
        public ServiceAgent()
        {
            this.ComputerName=new ComputerName();
            CheckHealth();
            GetHostName();
        }
       
        private void GetHostName()
        {
            Get["/hostname"] = parameters =>
            {
                var model = new HostNameModel {hostName = ComputerName.GetComputerName()};
                return Negotiate.WithStatusCode(HttpStatusCode.OK)
                                .WithModel(model);
               
            };
        }

        private void CheckHealth()
        {
            Get["/health"] = parameters => Negotiate.WithStatusCode(HttpStatusCode.OK);
        }
    }
}
