using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneClickDesktop.BackendClasses.Model.Resources
{
    /// <summary>
    /// Class describing basic resources
    /// </summary>
    public class Resources
    {
        /// <summary>
        /// Memory in MiB (1024 base)
        /// </summary>
        public int Memory { get; set; }

        /// <summary>
        /// Number of CPU threads
        /// </summary>
        public int CpuCores { get; set; }

        /// <summary>
        /// Storage in GiB (1024 base)
        /// </summary>
        public int Storage { get; set; }

        /// <summary>
        /// Create resources from numerical description
        /// </summary>
        /// <param name="memory">Amount of memory bytes assigned</param>
        /// <param name="cpuCores">Amount of CPU cpuCores assigned</param>
        /// <param name="storage">Amount of storage bytes assigned</param>
        public Resources(int memory, int cpuCores, int storage)
        {
            Memory = memory;
            CpuCores = cpuCores;
            Storage = storage;
        }
    }
}
