using System;
using System.Text.Json.Serialization;

namespace OneClickDesktop.BackendClasses.Model
{
    /// <summary>
    /// Stores information about user
    /// </summary>
    public class User : IComparable<User>, IEquatable<User>
    {
        /// <summary>
        /// User identifier
        /// </summary>
        public Guid Guid { get; }

        /// <summary>
        /// JWT token for authorization
        /// </summary>
        public string JwtToken { get; set; }

        /// <summary>
        /// Create new user object
        /// </summary>
        public User()
        {
            Guid = Guid.NewGuid();
            JwtToken = null;
        }

        /// <summary>
        /// Create new user with identifier and token
        /// </summary>
        /// <param name="guid">User identifier</param>
        /// <param name="jwtToken">User JWT token</param>
        [JsonConstructor]
        public User(Guid guid, string jwtToken = null)
        {
            Guid = guid;
            JwtToken = jwtToken;
        }

        /// <summary>
        /// Compares this object with other User object. Comparison is made on <see cref="Guid"/>
        /// </summary>
        /// <param name="other">User object to compare</param>
        /// <returns>Int representing position of this object compared to other</returns>
        public int CompareTo(User other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Guid.CompareTo(other.Guid);
        }

        /// <summary>
        /// Checks if other User object is equal to this one. Checks if <see cref="Guid"/> is equal
        /// </summary>
        /// <param name="other">User to check against</param>
        /// <returns>True if Users are equal, otherwise false</returns>
        public bool Equals(User other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Guid.Equals(other.Guid);
        }

        /// <summary>
        /// Returns the hash code of User
        /// </summary>
        /// <returns>32-bit signed integer hash code</returns>
        public override int GetHashCode()
        {
            return Guid.GetHashCode();
        }

        /// <summary>
        /// Checks if other object is equal to this one. Checks if <see cref="Guid"/> is equal
        /// </summary>
        /// <param name="obj">Object to check against</param>
        /// <returns>True if objects are equal, otherwise false</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((User)obj);
        }
    }
}