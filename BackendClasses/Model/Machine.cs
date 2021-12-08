using System;
using System.Net;
using System.Text.Json.Serialization;
using OneClickDesktop.BackendClasses.Model.Resources;
using OneClickDesktop.BackendClasses.Model.States;

namespace OneClickDesktop.BackendClasses.Model
{
    /// <summary>
    /// Single instance of virtual machine
    /// </summary>
    public class Machine: IComparable<Machine>, IEquatable<Machine>
    {
        /// <summary>
        /// Machine identifier
        /// </summary>
        public Guid Guid { get; }
        
        /// <summary>
        /// Current state of machine
        /// </summary>
        public MachineState State { get; set; }

        /// <summary>
        /// Type of machine
        /// </summary>
        public MachineType MachineType { get; }

        /// <summary>
        /// Resources assigned to machine
        /// </summary>
        public MachineResources UsingResources { get; }

        /// <summary>
        /// User currently using machine
        /// </summary>
        public User ConnectedUser { get; private set; }

        /// <summary>
        /// IpAddress of machine (for connection)
        /// </summary>
        public IPAddress IpAddress { get; private set; }
        
        /// <summary>
        /// Virtualization server hosting machine
        /// </summary>
        [JsonIgnore]
        public VirtualizationServer ParentServer { get; set; }
        
        /// <summary>
        /// Json constructor
        /// </summary>
        [JsonConstructor]
        public Machine(Guid guid, MachineState state, MachineType machineType, MachineResources usingResources, User connectedUser, IPAddress ipAddress)
        {
            Guid = guid;
            State = state;
            MachineType = machineType;
            UsingResources = usingResources;
            ConnectedUser = connectedUser;
            IpAddress = ipAddress;
        }
        
        /// <summary>
        /// Create machine in OFF state with no user assigned and no ipAddress (can only be assigned after machine starts)
        /// </summary>
        /// <param name="type">Machine Type</param>
        /// <param name="resources">Resources assigned to machine</param>
        /// <param name="parent">Virtualization server running machine</param>
        public Machine(MachineType type, MachineResources resources, VirtualizationServer parent)
        {
            Guid = Guid.NewGuid();
            MachineType = type;
            UsingResources = resources;
            ParentServer = parent;
            ConnectedUser = null;
            State = MachineState.TurnedOff;
            IpAddress = null;
        }

        /// <summary>
        /// Assign user to machine
        /// </summary>
        /// <param name="user">User to assign</param>
        public void AssignUser(User user) => ConnectedUser = user;

        /// <summary>
        /// Assign IP ipAddress to machine
        /// </summary>
        /// <param name="ipAddress">IP address of machine</param>
        public void AssignAddress(IPAddress ipAddress) => IpAddress = ipAddress;

        public int CompareTo(Machine other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Guid.CompareTo(other.Guid);
        }

        public bool Equals(Machine other)
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
            return Equals(obj as Machine);
        }
    }
}
