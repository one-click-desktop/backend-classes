using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace OneClickDesktop.BackendClasses.Model.Resources
{
    /// <summary>
    /// Class describing PCI identifier as pair of vendor and device identifiers
    /// </summary>
    public class PciAddressId: IEquatable<PciAddressId>
    {
        public string Domain { get; set; }
        public string Bus { get; set; }
        public string Slot { get; set; }
        public string Function { get; set; }
        
        
        /// <summary>
        /// Create PCIId representation from vendor and device id
        /// </summary>
        /// <param name="vendorId">vendor id</param>
        /// <param name="deviceId">device id</param>
        [JsonConstructor]
        public PciAddressId (string domain, string bus, string slot, string function)
        {
            Domain = domain;
            Bus = bus;
            Slot = slot;
            Function = function;
        }

        public static PciAddressId Parse()

        public bool Equals(PciAddressId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return vendorId == other.vendorId && deviceId == other.deviceId;
        }
        
        public override string ToString()
        {
            return $"{Domain}:{Bus}:{Slot}.{Function}";
        }

        public override bool Equals(object obj)
        {
            ;
            if ((obj as PciAddressId pci == null)
                return 
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
