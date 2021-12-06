using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneClickDesktop.BackendClasses.Model;
using OneClickDesktop.BackendClasses.Model.Resources;

namespace OneClickDesktop.BackendClasses.ModelTests.VirtualisationServerTests
{
    class BaseVirtualisationServerTest
    {
        protected VirtualizationServer virtsrv;

        protected MachineType GetMachineType(string type)
        {
            return new MachineType()
            {
                type = type
            };
        }

        protected MachineType GetCPUMachineType() => GetMachineType("cpu");
        protected MachineType GetGPUMachineType() => GetMachineType("gpu");
        protected MachineType GetUnknownMachineType() => GetMachineType("wtf");

        protected GpuId GetGtx970()
        {
            //Teoretycznie można przekazywac tylko procesor graficzny bez wszytskich dodatkow
            //TODO Piotrek! Trzeba z nim porozmawiac
            //                                        GM204                     HD Audio 
            return new GpuId(new PciId[] { new PciId("10de", "13c2"), new PciId("10de", "0fbb") });
        }



        protected VirtualizationServer PrepareVirtualisationServer()
        {
            ServerResources resource = new ServerResources(16 * 1024, 8, 1024, new List<GpuId>() { GetGtx970() });
            Dictionary<MachineType, TemplateResources> templates = new Dictionary<MachineType, TemplateResources>();
            templates[GetCPUMachineType()] = new TemplateResources(2 * 1024, 4, 200, false);
            templates[GetGPUMachineType()] = new TemplateResources(4 * 1024, 8, 200, true, GetGtx970());

            return new VirtualizationServer(resource, templates);

        }
    }
}
