using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace OneClickDesktop.BackendClasses.Model.Resources
{
    /// <summary>
    /// Class describing resources used by single virtualization server
    /// </summary>
    public class ServerResources : Resources, IEquatable<ServerResources>
    {
        /// <summary>
        /// Number of GPU processors
        /// </summary>
        [JsonIgnore]
        public int GpuCount => GpuIds.Count;

        /// <summary>
        /// Collection of identifiers of GPU - every GPU contains multiple PCI IDs
        /// </summary>
        public List<GpuId> GpuIds { get; }

        /// <summary>
        /// Json constructor
        /// </summary>
        [JsonConstructor]
        public ServerResources(int memory, int cpuCores, int storage, List<GpuId> gpuIds)
            : base(memory, cpuCores, storage)
        {
            GpuIds = gpuIds;
        }
        
        /// <summary>
        /// Create server resources from numerical description
        /// </summary>
        /// <param name="memory">Amount of memory bytes assigned</param>
        /// <param name="cpuCores">Amount of CPU cpuCores assigned</param>
        /// <param name="storage">Amount of storage bytes assigned</param>
        /// <param name="gpuIds">List of GPU identifiers</param>
        public ServerResources(int memory, int cpuCores, int storage, IEnumerable<GpuId> gpuIds)
            : base(memory, cpuCores, storage)
        {
            GpuIds = new List<GpuId>(gpuIds);
        }

        /// <summary>
        /// Create server resources from base resources and GPUs identifiers
        /// </summary>
        /// <param name="baseResources">Base resources</param>
        /// <param name="gpus">List of GPU descriptions</param>
        public ServerResources(Resources baseResources, IEnumerable<GpuId> gpus)
            : base(baseResources.Memory, baseResources.CpuCores, baseResources.Storage)
        {
            GpuIds = new List<GpuId>(gpus);
        }

        public bool Equals(ServerResources other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && GpuIds.SequenceEqual(other.GpuIds);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ServerResources)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), GpuIds);
        }
    }
}
