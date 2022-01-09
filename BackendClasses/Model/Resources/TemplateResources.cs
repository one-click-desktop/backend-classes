using System;
using System.Text.Json.Serialization;
using OneClickDesktop.BackendClasses.Model.Types;

namespace OneClickDesktop.BackendClasses.Model.Resources
{
    /// <summary>
    /// Class describing template resources used when creating machine
    /// </summary>
    public class TemplateResources : Resources, IEquatable<TemplateResources>
    {
        /// <summary>
        /// Machine type paired with this template
        /// </summary>
        public MachineType TemplateType { get; set; }

        /// <summary>
        /// Whether or not machine should have GPU attached
        /// </summary>
        public bool AttachGpu { get; set; }

        /// <summary>
        /// GPU description of GPU to attach (does not guarantee this GPU will be used)
        /// </summary>
        public string WishedGpuModel { get; set; }

        /// <summary>
        /// Create template resources from numerical description
        /// </summary>
        /// <param name="templateType"></param>
        /// <param name="memory">Amount of memory bytes assigned</param>
        /// <param name="cpuCores">Amount of CPU cpuCores assigned</param>
        /// <param name="storage">Amount of storage bytes assigned</param>
        /// <param name="attachGpu">Attach gpu to machine</param>
        /// <param name="wishedGpuModel">Description of wished GPU model</param>
        [JsonConstructor]
        public TemplateResources(MachineType templateType, int memory, int cpuCores, int storage, bool attachGpu,
                                 string wishedGpuModel = null)
            : base(memory, cpuCores, storage)
        {
            TemplateType = templateType;
            AttachGpu = attachGpu;
            WishedGpuModel = wishedGpuModel;
        }

        /// <summary>
        /// Checks if other TemplateResources object is equal to this one. Checks if all fields are equal
        /// </summary>
        /// <param name="other">TemplateResources to check against</param>
        /// <returns>True if TemplateResources are equal, otherwise false</returns>
        public bool Equals(TemplateResources other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && AttachGpu == other.AttachGpu && Equals(WishedGpuModel, other.WishedGpuModel) &&
                   TemplateType.Equals(other.TemplateType);
        }

        /// <summary>
        /// Checks if other object is equal to this one. Checks if all fields are equal
        /// </summary>
        /// <param name="obj">Object to check against</param>
        /// <returns>True if objects are equal, otherwise false</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TemplateResources)obj);
        }

        /// <summary>
        /// Returns the hash code of TemplateResources
        /// </summary>
        /// <returns>32-bit signed integer hash code</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), AttachGpu, WishedGpuModel);
        }
    }
}