using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneClickDesktop.BackendClasses.Model.Resources
{
    class ServerResources : Resources
    {
        /// <summary>
        /// Number of GPU processors
        /// </summary>
        public int GPUCount => GPUIds.Count();

        /// <summary>
        /// Collection of idetifiers of GPU - every GPU contains multiple PCI ID
        /// </summary>
        public List<GPUId> GPUIds { get; set; }
        
        public ServerResources(int memory, int cpuCores, int storage, IEnumerable<GPUId> gpus)
            : base(memory, cpuCores, storage)
        {
            GPUIds = new List<GPUId>(gpus);
        }
    }
}
