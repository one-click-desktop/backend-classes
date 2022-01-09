using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace OneClickDesktop.BackendClasses.Model.Resources
{

    /// <summary>
    /// Class describing GPU as collection of PCI identifiers
    /// </summary>
    public class GpuId: IEquatable<GpuId>
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

        public override string ToString()
        {
            return String.Join(",", PciIdentifiers);
        }

        public bool Equals(GpuId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return PciIdentifiers.SequenceEqual(other.PciIdentifiers);
        }

        public override int GetHashCode()
        {
            return PciIdentifiers.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as GpuId);
        }
    }
}
