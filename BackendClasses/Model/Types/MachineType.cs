using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OneClickDesktop.BackendClasses.Model
{
    /// <summary>
    /// Class describing type of domain
    /// </summary>
    public class MachineType
    {
        /// <summary>
        /// Technical named used inside model
        /// </summary>
        public string TechnicalName { get; set; }
        /// <summary>
        /// Human readable name displaying at frontend
        /// </summary>
        public string HumanReadableName { get; set; }
        
        public MachineType()
        {}

        [JsonConstructor]
        private MachineType(string technicalName, string humanReadableName)
        {
            TechnicalName = technicalName;
            HumanReadableName = humanReadableName;
        }

        public override bool Equals(object obj)
        {
            return obj is MachineType type &&
                   this.TechnicalName == type.TechnicalName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TechnicalName);
        }

        public override string ToString()
        {
            return TechnicalName;
        }
    }
}
