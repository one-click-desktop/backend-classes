using System;
using System.Text.Json.Serialization;
using OneClickDesktop.BackendClasses.Model.Resources;
using OneClickDesktop.BackendClasses.Model.States;
using OneClickDesktop.BackendClasses.Model.Types;

namespace OneClickDesktop.BackendClasses.Model
{
    /// <summary>
    /// Single instance of virtual machine
    /// </summary>
    public class Machine : IEquatable<Machine>
    {
        /// <summary>
        /// Machine identifier
        /// </summary>
        public string Name { get; }

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
        public MachineAddress? IpAddress { get; private set; }

        /// <summary>
        /// Virtualization server hosting machine
        /// </summary>
        [JsonIgnore]
        public VirtualizationServer ParentServer { get; set; }

        /// <summary>
        /// Json constructor
        /// </summary>
        [JsonConstructor]
        public Machine(string name, MachineState state, MachineType machineType, MachineResources usingResources,
                       User connectedUser, MachineAddress? ipAddress)
        {
            Name = name;
            State = state;
            MachineType = machineType;
            UsingResources = usingResources;
            ConnectedUser = connectedUser;
            IpAddress = ipAddress;
        }

        /// <summary>
        /// Create machine in OFF state with no user assigned and no ipAddress (can only be assigned after machine starts)
        /// </summary>
        /// <param name="name">Machine identifier</param>
        /// <param name="type">Machine Type</param>
        /// <param name="resources">Resources assigned to machine</param>
        /// <param name="parent">Virtualization server running machine</param>
        public Machine(string name, MachineType type, MachineResources resources, VirtualizationServer parent)
        {
            Name = name;
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
        /// <param name="ip">IP address of machine</param>
        public void AssignAddress(MachineAddress ip) => IpAddress = ip;

        /// <summary>
        /// Checks if other Machine object is equal to this one. Checks if <see cref="Name"/> is equal
        /// </summary>
        /// <param name="other">Machine to check against</param>
        /// <returns>True if Machines are equal, otherwise false</returns>
        public bool Equals(Machine other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name.Equals(other.Name);
        }

        /// <summary>
        /// Returns the hash code of Machine
        /// </summary>
        /// <returns>32-bit signed integer hash code</returns>
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        /// <summary>
        /// Checks if other object is equal to this one. Checks if <see cref="Name"/> is equal
        /// </summary>
        /// <param name="obj">Object to check against</param>
        /// <returns>True if objects are equal, otherwise false</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((Machine)obj);
        }
    }
}