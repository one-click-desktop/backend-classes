using System;
using System.Text.Json.Serialization;
using OneClickDesktop.BackendClasses.Model.States;
using OneClickDesktop.BackendClasses.Model.Types;

namespace OneClickDesktop.BackendClasses.Model
{
    /// <summary>
    /// Session connecting user to machine
    /// </summary>
    public class Session : IEquatable<Session>, IComparable<Session>
    {
        /// <summary>
        /// Machine assigned to session
        /// </summary>
        public Machine CorrelatedMachine { get; private set; }

        /// <summary>
        /// User assigned to session
        /// </summary>
        public User CorrelatedUser { get; }

        /// <summary>
        /// Session identifier
        /// </summary>
        public Guid SessionGuid { get; }

        /// <summary>
        /// Session Type
        /// </summary>
        public SessionType SessionType { get; }

        /// <summary>
        /// Session state
        /// </summary>
        public SessionState SessionState { get; set; }

        /// <summary>
        /// Json constructor
        /// </summary>
        [JsonConstructor]
        public Session(Machine correlatedMachine, User correlatedUser, Guid sessionGuid, SessionType sessionType,
                       SessionState sessionState)
        {
            CorrelatedMachine = correlatedMachine;
            CorrelatedUser = correlatedUser;
            SessionGuid = sessionGuid;
            SessionType = sessionType;
            SessionState = sessionState;
        }

        /// <summary>
        /// Create session of set Type for user and machine
        /// </summary>
        /// <param name="user">Session user</param>
        /// <param name="sessionType">Session Type</param>
        /// <param name="machine">Assigned machine (defaults to null)</param>
        public Session(User user, SessionType sessionType, Machine machine = null)
        {
            SessionGuid = Guid.NewGuid();
            SessionType = sessionType;
            SessionState = SessionState.Pending;
            CorrelatedUser = user;
            CorrelatedMachine = machine;
        }

        /// <summary>
        /// Create session from other session
        /// </summary>
        /// <param name="other">Other session</param>
        public Session(Session other)
        {
            SessionGuid = other.SessionGuid;
            SessionType = other.SessionType;
            SessionState = other.SessionState;
            CorrelatedUser = other.CorrelatedUser;
            CorrelatedMachine = other.CorrelatedMachine;
        }

        /// <summary>
        /// Assign machine to session
        /// </summary>
        /// <param name="machine">Machine for assign</param>
        public void AttachMachine(Machine machine)
        {
            if (machine.State != MachineState.Free)
            {
                throw new ArgumentException("Machine is not free", nameof(machine));
            }

            machine.State = MachineState.Reserved;
            CorrelatedMachine = machine;
        }

        /// <summary>
        /// Marks session as dead (remove machine and mark to remove)
        /// </summary>
        public void DetachMachine()
        {
            CorrelatedMachine = null;
            SessionState = SessionState.WaitingForRemoval;
        }

        /// <summary>
        /// Compares this object with other Session object. Comparison is made on <see cref="SessionGuid"/>
        /// </summary>
        /// <param name="other">Session object to compare</param>
        /// <returns>Int representing position of this object compared to other</returns>
        public int CompareTo(Session other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return SessionGuid.CompareTo(other.SessionGuid);
        }

        /// <summary>
        /// Checks if other Session object is equal to this one. Checks if <see cref="SessionGuid"/> is equal
        /// </summary>
        /// <param name="other">Session to check against</param>
        /// <returns>True if Sessions are equal, otherwise false</returns>
        public bool Equals(Session other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return SessionGuid.Equals(other.SessionGuid);
        }

        /// <summary>
        /// Returns the hash code of Session
        /// </summary>
        /// <returns>32-bit signed integer hash code</returns>
        public override int GetHashCode()
        {
            return SessionGuid.GetHashCode();
        }

        /// <summary>
        /// Checks if other object is equal to this one. Checks if <see cref="SessionGuid"/> is equal
        /// </summary>
        /// <param name="obj">Object to check against</param>
        /// <returns>True if objects are equal, otherwise false</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((Session)obj);
        }
    }
}