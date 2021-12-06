using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneClickDesktop.BackendClasses.Model.Resources
{
    /// <summary>
    /// Class describing template resources used when creating machine
    /// </summary>
    public class TemplateResources: Resources
    {
        /// <summary>
        /// Whether or not machine should have GPU attached
        /// </summary>
        public bool ShouldAttachGpu { get; set; }
        
        /// <summary>
        /// GPU description of GPU to attach (does not guarantee this GPU will be used)
        /// </summary>
        public GpuId WishedGpuModel { get; set; }

        /// <summary>
        /// Create template resources from numerical description
        /// </summary>
        /// <param name="memory">Amount of memory bytes assigned</param>
        /// <param name="cpuCores">Amount of CPU cpuCores assigned</param>
        /// <param name="storage">Amount of storage bytes assigned</param>
        /// <param name="attachGpu">Attach gpu to machine</param>
        /// <param name="wishedModel">Description of wished GPU model</param>
        public TemplateResources(int memory, int cpuCores, int storage, bool attachGpu, GpuId wishedModel = null)
            : base(memory, cpuCores, storage)
        {
            ShouldAttachGpu = attachGpu;
            WishedGpuModel = wishedModel;
        }
    }
}
