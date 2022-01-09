using System;

namespace OneClickDesktop.BackendClasses.Model.Types
{
    /// <summary>
    /// Class describing type of domain
    /// </summary>
    public class MachineType : IEquatable<MachineType>, IEquatable<SessionType>, IEquatable<string>
    {
        /// <summary>
        /// Technical named used inside model
        /// </summary>
        public string TechnicalName { get; set; }

        /// <summary>
        /// Human readable name displaying at frontend
        /// </summary>
        public string HumanReadableName { get; set; }

        /// <summary>
        /// Checks if other object is equal to this one. Checks if all fields are equal
        /// </summary>
        /// <param name="obj">Object to check against</param>
        /// <returns>True if objects are equal, otherwise false</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((MachineType)obj);
        }

        /// <summary>
        /// Returns the hash code of MachineType
        /// </summary>
        /// <returns>32-bit signed integer hash code</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(TechnicalName, HumanReadableName);
        }

        /// <summary>
        /// String representation of MachineType
        /// </summary>
        /// <returns>Technical name</returns>
        public override string ToString()
        {
            return TechnicalName;
        }

        /// <summary>
        /// Checks if other MachineType object is equal to this one. Checks if all fields are equal
        /// </summary>
        /// <param name="other">MachineType to check against</param>
        /// <returns>True if MachineTypes are equal, otherwise false</returns>
        public bool Equals(MachineType other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return TechnicalName == other.TechnicalName && HumanReadableName == other.HumanReadableName;
        }

        /// <summary>
        /// Checks if other SessionType object is equal to this one. Objects are equal if
        /// <see cref="TechnicalName"/> is equal to <see cref="SessionType.Type"/>
        /// </summary>
        /// <param name="other">SessionType to check against</param>
        /// <returns>True if objects are equal, otherwise false</returns>
        public bool Equals(SessionType other)
        {
            if (ReferenceEquals(null, other)) return false;
            return TechnicalName == other.Type;
        }

        /// <summary>
        /// Checks if string object is equal to this one. Objects are equal if
        /// string is equal to <see cref="TechnicalName"/>
        /// </summary>
        /// <param name="other">string to check against</param>
        /// <returns>True if objects are equal, otherwise false</returns>
        public bool Equals(string other)
        {
            if (ReferenceEquals(null, other)) return false;
            return TechnicalName == other;
        }
    }
}