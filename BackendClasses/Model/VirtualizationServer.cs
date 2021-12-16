using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using OneClickDesktop.BackendClasses.Model.Resources;
using OneClickDesktop.BackendClasses.Model.States;

namespace OneClickDesktop.BackendClasses.Model
{
    /// <summary>
    /// Represents virtualization server
    /// </summary>
    public class VirtualizationServer : IEquatable<VirtualizationServer>, IComparable<VirtualizationServer>
    {
        private readonly Dictionary<Guid, Session> sessions = new Dictionary<Guid, Session>();
        private readonly Dictionary<string, Machine> runningMachines = new Dictionary<string, Machine>();
        private readonly Dictionary<string, TemplateResources> templateResources;

        /// <summary>
        /// Server identifier
        /// </summary>
        public Guid ServerGuid { get; }

        /// <summary>
        /// Sessions running on server
        /// </summary>
        public IReadOnlyDictionary<Guid, Session> Sessions => sessions;

        /// <summary>
        /// Complete resources owned by server
        /// </summary>
        public ServerResources TotalResources { get; }

        /// <summary>
        /// Free resources on server (resources not used by any machine)
        /// </summary>
        [JsonIgnore]
        public ServerResources FreeResources
        {
            get
            {
                var machines = runningMachines.Values.Where(machine => machine.State != MachineState.TurnedOff)
                                              .ToArray();
                var resources = machines.Select(machine => machine.UsingResources as Resources.Resources)
                                        .DefaultIfEmpty(new Resources.Resources(0, 0, 0))
                                        .Aggregate((resources1, resources2) => resources1 + resources2);
                var gpusUsed = machines.Select(machine => machine.UsingResources?.Gpu);
                var gpus = TotalResources.GpuIds.Except(gpusUsed);
                return new ServerResources(TotalResources - resources, gpus);
            }
        }

        /// <summary>
        /// Available resources on server (free resources + free machines)
        /// </summary>
        [JsonIgnore]
        public ServerResources AvailableResources
        {
            get
            {
                var machines = runningMachines.Values
                                              .Where(machine => machine.State != MachineState.Free &&
                                                                machine.State != MachineState.TurnedOff
                                              ).ToArray();
                var resources = machines.Select(machine => machine.UsingResources as Resources.Resources)
                                        .DefaultIfEmpty(new Resources.Resources(0, 0, 0))
                                        .Aggregate((resources1, resources2) => resources1 + resources2);
                var gpusUsed = machines.Select(machine => machine.UsingResources?.Gpu);
                var gpus = TotalResources.GpuIds.Except(gpusUsed);
                return new ServerResources(TotalResources - resources, gpus);
            }
        }

        /// <summary>
        /// Template resources for machine Type
        /// </summary>
        public IReadOnlyDictionary<string, TemplateResources> TemplateResources => templateResources;

        /// <summary>
        /// Machines running on server
        /// </summary>
        public IReadOnlyDictionary<string, Machine> RunningMachines => runningMachines;

        /// <summary>
        /// Name of RabbitMQ queue for direct communication
        /// </summary>
        public string Queue { get; }

        /// <summary>
        /// Can server be managed (set after 2nd update)
        /// </summary>
        [JsonIgnore]
        public bool Managable { get; set; } = false;

        /// <summary>
        /// Json constructor
        /// </summary>
        [JsonConstructor]
        public VirtualizationServer(IReadOnlyDictionary<Guid, Session> sessions,
                                    IReadOnlyDictionary<string, Machine> runningMachines,
                                    IReadOnlyDictionary<string, TemplateResources> templateResources,
                                    ServerResources totalResources,
                                    Guid serverGuid,
                                    string queue)
        {
            this.sessions = new Dictionary<Guid, Session>(sessions);
            this.runningMachines = new Dictionary<string, Machine>(runningMachines);
            this.templateResources = new Dictionary<string, TemplateResources>(templateResources);
            ServerGuid = serverGuid;
            TotalResources = totalResources;
            Queue = queue;

            foreach (var machine in this.runningMachines.Values)
            {
                machine.ParentServer = this;
            }
        }

        /// <summary>
        /// Create virtualization server with complete resources and templates
        /// </summary>
        /// <param name="totalResources">Whole resources owned by server</param>
        /// <param name="templates">Template resources for use when creating new machines</param>
        /// <param name="queue">Name of RabbitMQ queue</param>
        public VirtualizationServer(ServerResources totalResources,
                                    IDictionary<string, TemplateResources> templates, string queue)
        {
            ServerGuid = Guid.NewGuid();
            TotalResources = totalResources;
            Queue = queue;
            this.templateResources = new Dictionary<string, TemplateResources>(templates);
        }

        /// <summary>
        /// Create new machine of specified Type with GPU
        /// </summary>
        /// <param name="name">Machine identifier</param>
        /// <param name="type">Type of machine</param>
        /// <param name="gpuId">Identifier of GPU to use. If null - uses GPU from template resources</param>
        /// <returns>Created machine</returns>
        /// <exception cref="ArgumentException">Invalid machine Type</exception>
        public Machine CreateMachine(string name, MachineType type, GpuId gpuId = null)
        {
            var template = templateResources.GetValueOrDefault(type.Type, null);
            if (template == null)
            {
                throw new ArgumentException("Invalid machine Type", nameof(type));
            }

            var resources = gpuId != null
                ? new MachineResources(template, gpuId)
                : new MachineResources(template);
            var machine = new Machine(name, type, resources, this);
            runningMachines.Add(machine.Name, machine);
            return machine;
        }

        /// <summary>
        /// Delete machine
        /// </summary>
        /// <param name="name">Machine identifier</param>
        public void DeleteMachine(string name) => runningMachines.Remove(name);

        /// <summary>
        /// Create full session on server with selected machine
        /// </summary>
        /// <param name="halfSession">Partial session (without machine)</param>
        /// <param name="machineName">Machine identifier</param>
        /// <returns>Session with attached machine</returns>
        /// <exception cref="ArgumentException">Session already on server or invalid guid:
        /// part of other session or doesn't exist</exception>
        public Session CreateFullSession(Session halfSession, string machineName)
        {
            if (sessions.ContainsKey(halfSession.SessionGuid))
            {
                throw new ArgumentException("This session already exists on server", nameof(halfSession));
            }

            if (!runningMachines.TryGetValue(machineName, out var machine))
            {
                throw new ArgumentException("Cannot find machine with specified guid on server", nameof(machineName));
            }

            if (sessions.Values.Any(session => machineName.Equals(session?.CorrelatedMachine?.Name)))
            {
                throw new ArgumentException("Machine already part of session", nameof(machineName));
            }

            halfSession.AttachMachine(machine);
            halfSession.SessionState = SessionState.Running;
            sessions.Add(halfSession.SessionGuid, halfSession);
            return halfSession;
        }

        public int CompareTo(VirtualizationServer other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return ServerGuid.CompareTo(other.ServerGuid);
        }

        public bool Equals(VirtualizationServer other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ServerGuid.Equals(other.ServerGuid);
        }

        public override int GetHashCode()
        {
            return ServerGuid.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as VirtualizationServer);
        }
    }
}