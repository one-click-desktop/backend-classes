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
        /// <param name="memory">Amount of memory MiB assigned</param>
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

        /// <summary>
        /// Calculates sum of server resources
        /// </summary>
        /// <param name="r1">Base server resources</param>
        /// <param name="r2">Added server resources</param>
        /// <returns>Sum of resources</returns>
        /// <remarks>Only use for statistics, since gpuIds don't make sense when outside of server</remarks>
        public static ServerResources operator +(ServerResources r1, ServerResources r2)
        {
            return new ServerResources((r1 as Resources) + r2,
                                       // this is not really correct but needed for GpuCount to work correctly
                                       new List<GpuId>(r1.GpuIds).Concat(r2.GpuIds));
        }

        /// <summary>
        /// Checks if other ServerResources object is equal to this one. Checks if all fields are equal and <see cref="GpuIds"/> contains same gpus
        /// </summary>
        /// <param name="other">ServerResources to check against</param>
        /// <returns>True if ServerResources are equal, otherwise false</returns>
        public bool Equals(ServerResources other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && GpuIds.SequenceEqual(other.GpuIds);
        }

        /// <summary>
        /// Checks if other object is equal to this one. Checks if all fields are equal and <see cref="GpuIds"/> contains same gpus
        /// </summary>
        /// <param name="obj">Object to check against</param>
        /// <returns>True if objects are equal, otherwise false</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((ServerResources)obj);
        }

        /// <summary>
        /// Returns the hash code of ServerResources
        /// </summary>
        /// <returns>32-bit signed integer hash code</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), GpuIds);
        }
    }
}