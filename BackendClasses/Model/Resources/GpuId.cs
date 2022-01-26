using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using static System.String;

namespace OneClickDesktop.BackendClasses.Model.Resources
{
    /// <summary>
    /// Class describing GPU as collection of PCI identifiers
    /// </summary>
    public class GpuId : IEquatable<GpuId>
    {
        /// <summary>
        /// List of PCI identifiers
        /// </summary>
        public List<PciAddressId> PciIdentifiers { get; }

        /// <summary>
        /// Json constructor
        /// </summary>
        [JsonConstructor]
        public GpuId(List<PciAddressId> pciIdentifiers)
        {
            PciIdentifiers = pciIdentifiers;
        }

        /// <summary>
        /// Create GpuId object
        /// </summary>
        /// <param name="pciIdentifiers">List of PCI identifiers</param>
        public GpuId(IEnumerable<PciAddressId> pciIdentifiers)
        {
            PciIdentifiers = new List<PciAddressId>(pciIdentifiers);
        }

        /// <summary>
        /// Concatenate PCI ids into string with ',' as separator
        /// </summary>
        /// <returns>String representation of Gpu</returns>
        public override string ToString()
        {
            return Join(",", PciIdentifiers);
        }

        /// <summary>
        /// Checks if other GpuId object is equal to this one. Checks if <see cref="PciIdentifiers"/> contains same identifiers
        /// </summary>
        /// <param name="other">GpuId to check against</param>
        /// <returns>True if GpuId are equal, otherwise false</returns>
        public bool Equals(GpuId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return PciIdentifiers.SequenceEqual(other.PciIdentifiers);
        }

        /// <summary>
        /// Returns the hash code of GpuId
        /// </summary>
        /// <returns>32-bit signed integer hash code</returns>
        public override int GetHashCode()
        {
            return PciIdentifiers.Aggregate(0, (sum, id) => sum ^ id.GetHashCode());
        }

        /// <summary>
        /// Checks if other object is equal to this one. Checks if <see cref="PciIdentifiers"/> contains same identifiers
        /// </summary>
        /// <param name="obj">Object to check against</param>
        /// <returns>True if objects are equal, otherwise false</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((GpuId)obj);
        }
    }
}