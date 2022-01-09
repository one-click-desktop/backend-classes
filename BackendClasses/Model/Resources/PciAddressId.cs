using System;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace OneClickDesktop.BackendClasses.Model.Resources
{
    /// <summary>
    /// Class describing PCI identifier as combination of domain, bus, slot and function ids. Format is similar to lspci
    /// </summary>
    public class PciAddressId : IEquatable<PciAddressId>
    {
        private static readonly Regex AddressRegexp =
            new Regex(
                @"^(?<domain>[0-9a-fA-F]{4}):(?<bus>[0-9a-fA-F]{2}):(?<slot>[0-9a-fA-F]{2})\.(?<function>[0-9a-fA-F]{1})$");

        /// <summary>
        /// PCI device domain
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// PCI device bus
        /// </summary>
        public string Bus { get; set; }

        /// <summary>
        /// PCI device slot
        /// </summary>
        public string Slot { get; set; }

        /// <summary>
        /// PCI device function
        /// </summary>
        public string Function { get; set; }


        /// <summary>
        /// Create PCI address id representation
        /// </summary>
        /// <param name="domain">Domain id</param>
        /// <param name="bus">Bus id</param>
        /// <param name="slot">Slot id</param>
        /// <param name="function">Function id</param>
        [JsonConstructor]
        public PciAddressId(string domain, string bus, string slot, string function)
        {
            Domain = domain;
            Bus = bus;
            Slot = slot;
            Function = function;
        }

        /// <summary>
        /// Parse from string matching same format as in <see cref="TryParse"/>
        /// </summary>
        /// <param name="address">String representation of PCI address</param>
        /// <returns>PCI address</returns>
        /// <exception cref="FormatException">String does not match format</exception>
        public static PciAddressId Parse(string address)
        {
            if (!TryParse(address, out var pciAddressId))
            {
                throw new FormatException("Address has invalid format");
            }

            return pciAddressId;
        }

        /// <summary>
        /// Try to parse PCI address from string representation in format '{domain:4}:{bus:2}:{slot:2}.{function:1}'.
        /// All groups are hexadecimal numbers without prefix
        /// </summary>
        /// <param name="address">String representation of PCI address</param>
        /// <param name="pciAddressId">Parsed PCI address if successful, otherwise null</param>
        /// <returns>Bool indicating whether parse succeed</returns>
        public static bool TryParse(string address, out PciAddressId pciAddressId)
        {
            pciAddressId = null;

            var match = AddressRegexp.Match(address);
            if (!match.Success)
            {
                return false;
            }

            pciAddressId = new PciAddressId(match.Groups["domain"].Value, match.Groups["bus"].Value,
                                            match.Groups["slot"].Value, match.Groups["function"].Value);
            return true;
        }

        /// <summary>
        /// Checks if other PCI address is equal to this one. Comparison is based on string representation of PCI address
        /// </summary>
        /// <param name="other">PCI address to check against</param>
        /// <returns>True if PCI addresses are equal, otherwise false</returns>
        public bool Equals(PciAddressId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ToString() == other.ToString();
        }

        /// <summary>
        /// Converts PCI address to string representation in format '{domain}:{bus}:{slot}.{function}'
        /// </summary>
        /// <returns>PCI address string representation</returns>
        public override string ToString()
        {
            return $"{Domain}:{Bus}:{Slot}.{Function}";
        }

        /// <summary>
        /// Checks if other object is equal to this one. Comparison is based on string representation of PCI address
        /// </summary>
        /// <param name="obj">Object to check against</param>
        /// <returns>True if objects are equal, otherwise false</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((PciAddressId)obj);
        }

        /// <summary>
        /// Returns the hash code of this PCI address
        /// </summary>
        /// <returns>32-bit signed integer hash code</returns>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}