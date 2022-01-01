using System;
using System.Text.Json.Serialization;
using OneClickDesktop.BackendClasses.Model.States;

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
        public Session(Machine correlatedMachine, User correlatedUser, Guid sessionGuid, SessionType sessionType, SessionState sessionState)
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
        
        public int CompareTo(Session other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return SessionGuid.CompareTo(other.SessionGuid);
        }

        public bool Equals(Session other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return SessionGuid.Equals(other.SessionGuid);
        }

        public override int GetHashCode()
        {
            return SessionGuid.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Session);
        }
    }
}