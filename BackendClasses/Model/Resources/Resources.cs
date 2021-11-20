using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneClickDesktop.BackendClasses.Model.Resources
{
    public class Resources
    {
        /// <summary>
        /// Memory in MiB (1024 base)
        /// </summary>
        public int Memory { get; set; }

        /// <summary>
        /// Number of CPU threads
        /// </summary>
        public int CPUCores { get; set; }

        /// <summary>
        /// Storage in GiB (1024 base)
        /// </summary>
        public int Storage { get; set; }


        public Resources(int memory, int cores, int storage)
        {
            Memory = memory;
            CPUCores = cores;
            Storage = storage;
        }
    }
}
