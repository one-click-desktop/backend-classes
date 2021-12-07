using System;
using System.Text.Json.Serialization;

namespace OneClickDesktop.BackendClasses.Model.Resources
{
    /// <summary>
    /// Class describing basic resources
    /// </summary>
    public class Resources : IEquatable<Resources>
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
        [JsonConstructor]
        public Resources(int memory, int cpuCores, int storage)
        {
            Memory = memory;
            CpuCores = cpuCores;
            Storage = storage;
        }

        public static Resources operator -(Resources r1, Resources r2)
        {
            return new Resources(r1.Memory - r2.Memory,
                                 r1.CpuCores - r2.CpuCores,
                                 r1.Storage - r2.Storage);
        }
        
        public static Resources operator +(Resources r1, Resources r2)
        {
            return new Resources(r1.Memory + r2.Memory,
                                 r1.CpuCores + r2.CpuCores,
                                 r1.Storage + r2.Storage);
        }

        public bool Equals(Resources other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Memory == other.Memory && CpuCores == other.CpuCores && Storage == other.Storage;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Resources)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Memory, CpuCores, Storage);
        }
    }
}