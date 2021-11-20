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
    }
}
