using System.Collections.Generic;
using OneClickDesktop.BackendClasses.Model;
using OneClickDesktop.BackendClasses.Model.Resources;

namespace OneClickDesktop.BackendClasses.ModelTests.VirtualizationServerTests
{
    internal class BaseVirtualizationServerTest
    {
        protected VirtualizationServer server;
        
        protected MachineType GetMachineType(string type)
        {
            return new MachineType()
            {
                TechnicalName = type,
                HumanReadableName = type + "HR"
            };
        }

        protected MachineType GetCpuMachineType() => GetMachineType("cpu");
        protected MachineType GetGpuMachineType() => GetMachineType("gpu");
        protected MachineType GetUnknownMachineType() => GetMachineType("wtf");

        protected GpuId GetGtx970()
        {
            //Teoretycznie można przekazywac tylko procesor graficzny bez wszytskich dodatkow
            //                                        GM204                     HD Audio 
            return new GpuId(new PciId[] { new PciId("10de", "13c2"), new PciId("10de", "0fbb") });
        }

        protected VirtualizationServer PrepareVirtualizationServer()
        {
            ServerResources resource = new ServerResources(16 * 1024, 8, 1024, new List<GpuId>() { GetGtx970() });
            Dictionary<string, TemplateResources> templates = new Dictionary<string, TemplateResources>();
            templates[GetCpuMachineType().TechnicalName] = new TemplateResources(GetCpuMachineType(),2 * 1024, 4, 200, false);
            templates[GetGpuMachineType().TechnicalName] = new TemplateResources(GetGpuMachineType(),4 * 1024, 8, 200, true, GetGtx970());

            return new VirtualizationServer(resource, templates, null);

        }
    }
}
