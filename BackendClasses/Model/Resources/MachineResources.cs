using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneClickDesktop.BackendClasses.Model.Resources
{
    /// <summary>
    /// Class describing resources used be single virtual machine
    /// </summary>
    class MachineResources: Resources
    {
        /// <summary>
        /// Assigned GPU processor
        /// </summary>
        public GPUId GPU { get; set; }

        public MachineResources(int memory, int cpuCores, int storage, GPUId gpu)
            : base(memory, cpuCores, storage)
        {
            GPU = gpu;
        }

        /// <summary>
        /// Create machine resources from template
        /// </summary>
        /// <param name="template">Template resources</param>
        /// <param name="gpu">Assigned resources</param>
        public MachineResources(Resources template, GPUId gpu)
            : base(template.Memory, template.CPUCores, template.Storage)
        {
            GPU = gpu;
        }
    }
}
