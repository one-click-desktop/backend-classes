using System;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace OneClickDesktop.BackendClasses.Model.Resources
{
    /// <summary>
    /// Class describing PCI identifier as pair of vendor and device identifiers
    /// </summary>
    public class PciAddressId : IEquatable<PciAddressId>
    {
        private static readonly Regex addressRegexp =
            new Regex(
                @"^(?<domain>[0-9a-fA-F]{4}):(?<bus>[0-9a-fA-F]{2}):(?<slot>[0-9a-fA-F]{2})\.(?<function>[0-9a-fA-F]{1})$");

        public string Domain { get; set; }
        public string Bus { get; set; }
        public string Slot { get; set; }
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
        /// All groups are hexadecimal numbers without prefix.
        /// </summary>
        /// <param name="address">String representation of PCI address</param>
        /// <param name="pciAddressId">Parsed PCI address if successful, otherwise null</param>
        /// <returns>Bool indicating whether parse successed</returns>
        public static bool TryParse(string address, out PciAddressId pciAddressId)
        {
            pciAddressId = null;

            var match = addressRegexp.Match(address);
            if (!match.Success)
            {
                return false;
            }

            pciAddressId = new PciAddressId(match.Groups["domain"].Value, match.Groups["bus"].Value,
                                            match.Groups["slot"].Value, match.Groups["function"].Value);
            return true;
        }

        public bool Equals(PciAddressId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ToString() == other.ToString();
        }

        public override string ToString()
        {
            return $"{Domain}:{Bus}:{Slot}.{Function}";
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((PciAddressId)obj);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}