using System;

namespace OneClickDesktop.BackendClasses.Model
{
    /// <summary>
    /// Stores information about user
    /// </summary>
    public class User: IComparable<User>, IEquatable<User>
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
        public User(Guid guid, string jwtToken = null)
        {
            Guid = guid;
            JwtToken = jwtToken;
        }

        public int CompareTo(User other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Guid.CompareTo(other.Guid);
        }

        public bool Equals(User other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Guid.Equals(other.Guid);
        }

        public override int GetHashCode()
        {
            return Guid.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as User);
        }
    }
}
