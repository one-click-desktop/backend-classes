namespace OneClickDesktop.BackendClasses.Model.Resources
{
    /// <summary>
    /// Class describing resources used by single virtual machine
    /// </summary>
    public class MachineResources: Resources
    {
        /// <summary>
        /// Assigned GPU processor
        /// </summary>
        public GpuId Gpu { get; set; }

        /// <summary>
        /// Create machine resources from numerical description
        /// </summary>
        /// <param name="memory">Amount of memory bytes assigned</param>
        /// <param name="cpuCores">Amount of CPU cores assigned</param>
        /// <param name="storage">Amount of storage bytes assigned</param>
        /// <param name="gpu">GPU assigned</param>
        public MachineResources(int memory, int cpuCores, int storage, GpuId gpu)
            : base(memory, cpuCores, storage)
        {
            Gpu = gpu;
        }

        /// <summary>
        /// Create machine resources from template
        /// </summary>
        /// <param name="template">Template resources</param>
        /// <param name="gpu">GPU assigned</param>
        public MachineResources(Resources template, GpuId gpu)
            : base(template.Memory, template.CpuCpuCores, template.Storage)
        {
            Gpu = gpu;
        }

        /// <summary>
        /// Create machine resources from template resource containing information about GPU
        /// </summary>
        /// <param name="template"></param>
        public MachineResources(TemplateResources template)
            : this(template, template.WishedGpuModel)
        {
            
        }
    }
}
