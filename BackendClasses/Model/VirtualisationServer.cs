using OneClickDesktop.BackendClasses.Model.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneClickDesktop.BackendClasses.Model
{
    
    class VirtualisationServer
    {
        public Dictionary<string, Session> SessionsOnServer { get; set; }
        public ServerResources WholeServerResources { get; set; }
        public Dictionary<MachineType, MachineResources> TemplateResources { get; set; }
        public Dictionary<string, Machine> RunningMachines { get; set; }

        public VirtualisationServer(ServerResources wholeResources, Dictionary<MachineType, MachineResources> templates)
        {
            WholeServerResources = wholeResources;
            TemplateResources = templates;
        }

        public void CreateMachine(string name, MachineType)
        {

        }

        public void ShutdownMachine(string name)
        {

        }

        public ServerResources CountFreeResources()
        {
            return null;
        }

        public Session CreateFullSession(Session halfSession, string machineName)
        {
            return null;
        }
    }
}
