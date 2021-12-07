using System;
using System.Collections.Generic;
using System.Linq;

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
        public List<PciId> PciIdentifiers { get; }

        /// <summary>
        /// Create GpuId object
        /// </summary>
        /// <param name="identifiers">List of PCI identifiers</param>
        public GpuId(IEnumerable<PciId> identifiers)
        {
            PciIdentifiers = new List<PciId>(identifiers);
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
