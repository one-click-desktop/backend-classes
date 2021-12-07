using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace OneClickDesktop.BackendClasses.Model.Resources
{
    /// <summary>
    /// Class describing PCI identifier as pair of vendor and device identifiers
    /// </summary>
    public class PciId: IEquatable<PciId>
    {
        private string vendorId;
        private string deviceId;
        
        /// <summary>
        /// Manufacturer unique id
        /// </summary>
        /// <exception cref="ArgumentException">Vendor ID must be lowercase 4 digit hex number</exception>
        /// 
        public string VendorId
        {
            get => vendorId;
            set
            {
                if (!IsValidId(value))
                    throw new ArgumentException($"Vendor ID {value} must be lowercase 4 digit hex number");
                vendorId = value;
            }
        }
        
        /// <summary>
        /// Unique Device ID inside manufacturer space
        /// </summary>
        /// <exception cref="ArgumentException">Device ID must be lowercase 4 digit hex number</exception>
        public string DeviceId 
        { 
            get => deviceId;
            set
            {
                if (!IsValidId(value))
                    throw new ArgumentException($"Device ID {value} must be lowercase 4 digit hex number");
                deviceId = value;
            }
        }

        /// <summary>
        /// Create PCIId representation from vendor and device id
        /// </summary>
        /// <param name="vendorId">vendor id</param>
        /// <param name="deviceId">device id</param>
        [JsonConstructor]
        public PciId (string vendorId, string deviceId)
        {
            VendorId = vendorId;
            DeviceId = deviceId;
        }

        /// <summary>
        /// Checks if given string is valid id.
        /// Valid id must:
        /// 1. Has 4 characters
        /// 2. Be lowercase hexadecimal number
        /// </summary>
        /// <param name="id">id to test</param>
        /// <returns>true - valid id, false - otherwise</returns>
        public static bool IsValidId(string id)
        {
            // has length 4 and is lowercase hexadecimal
            return id.Length == 4 && id.All(c => c is >= '0' and <= '9' or >= 'a' and <= 'f');
        }

        public bool Equals(PciId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return vendorId == other.vendorId && deviceId == other.deviceId;
        }
        
        public override string ToString()
        {
            return $"{VendorId}:{DeviceId}";
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as PciId);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(vendorId, deviceId);
        }
    }
}
