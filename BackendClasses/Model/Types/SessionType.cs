using System;

namespace OneClickDesktop.BackendClasses.Model.Types
{
    /// <summary>
    /// Class describing type of session
    /// </summary>
    public class SessionType : IEquatable<SessionType>, IEquatable<MachineType>
    {
        /// <summary>
        /// Session type code
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Returns the hash code of SessionType
        /// </summary>
        /// <returns>32-bit signed integer hash code</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Type);
        }

        /// <summary>
        /// String representation of SessionType
        /// </summary>
        /// <returns>Session type</returns>
        public override string ToString()
        {
            return Type;
        }

        /// <summary>
        /// Checks if other object object is equal to this one. Checks if all fields are equal
        /// </summary>
        /// <param name="obj">Object to check against</param>
        /// <returns>True if objects are equal, otherwise false</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((SessionType)obj);
        }

        /// <summary>
        /// Checks if other SessionType object is equal to this one. Checks if all fields are equal
        /// </summary>
        /// <param name="other">SessionType to check against</param>
        /// <returns>True if SessionTypes are equal, otherwise false</returns>
        public bool Equals(SessionType other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Type == other.Type;
        }

        /// <summary>
        /// Checks if other MachineType object is equal to this one. Objects are equal if
        /// <see cref="Type"/> is equal to <see cref="MachineType.TechnicalName"/>
        /// </summary>
        /// <param name="other">MachineType to check against</param>
        /// <returns>True if objects are equal, otherwise false</returns>
        public bool Equals(MachineType other)
        {
            if (ReferenceEquals(null, other)) return false;
            return Type == other.TechnicalName;
        }
    }
}