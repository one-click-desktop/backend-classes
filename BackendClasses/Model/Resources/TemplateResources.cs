using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneClickDesktop.BackendClasses.Model.Resources
{
    public class TemplateResources: Resources
    {
        public bool ShouldAttachGPU { get; set; }
        public GPUId WishedGPUModel { get; set; }

        public TemplateResources(int memory, int cpuCores, int storage, bool attachGpu, GPUId wishedModel = null)
            : base(memory, cpuCores, storage)
        {
            ShouldAttachGPU = attachGpu;
            WishedGPUModel = wishedModel;
        }
    }
}
