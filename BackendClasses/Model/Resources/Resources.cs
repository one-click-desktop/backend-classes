using System;
using System.Text.Json.Serialization;

namespace OneClickDesktop.BackendClasses.Model.Resources
{
    /// <summary>
    /// Class describing basic resources used by machine
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
        /// <param name="memory">Amount of memory MiB assigned</param>
        /// <param name="cpuCores">Amount of CPU cpuCores assigned</param>
        /// <param name="storage">Amount of storage bytes assigned</param>
        [JsonConstructor]
        public Resources(int memory, int cpuCores, int storage)
        {
            Memory = memory;
            CpuCores = cpuCores;
            Storage = storage;
        }

        /// <summary>
        /// Calculates difference of resources
        /// </summary>
        /// <param name="r1">Base resources</param>
        /// <param name="r2">Subtracted resources</param>
        /// <returns>Difference of resources</returns>
        /// <remarks>Does not check if result has negative fields</remarks>
        public static Resources operator -(Resources r1, Resources r2)
        {
            return new Resources(r1.Memory - r2.Memory,
                                 r1.CpuCores - r2.CpuCores,
                                 r1.Storage - r2.Storage);
        }

        /// <summary>
        /// Calculates sum of resources
        /// </summary>
        /// <param name="r1">Base resources</param>
        /// <param name="r2">Added resources</param>
        /// <returns>Sum of resources</returns>
        public static Resources operator +(Resources r1, Resources r2)
        {
            return new Resources(r1.Memory + r2.Memory,
                                 r1.CpuCores + r2.CpuCores,
                                 r1.Storage + r2.Storage);
        }

        /// <summary>
        /// Checks if other Resources object is equal to this one. Checks if all fields are equal
        /// </summary>
        /// <param name="other">Resources to check against</param>
        /// <returns>True if Resources are equal, otherwise false</returns>
        public bool Equals(Resources other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Memory == other.Memory && CpuCores == other.CpuCores && Storage == other.Storage;
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
            return obj.GetType() == this.GetType() && Equals((Resources)obj);
        }

        /// <summary>
        /// Returns the hash code of Resources
        /// </summary>
        /// <returns>32-bit signed integer hash code</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Memory, CpuCores, Storage);
        }
    }
}