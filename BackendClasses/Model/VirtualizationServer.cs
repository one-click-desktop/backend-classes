using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OneClickDesktop.BackendClasses.Model.Resources;

namespace OneClickDesktop.BackendClasses.Model
{
    /// <summary>
    /// Represents virtualization server
    /// </summary>
    public class VirtualizationServer : IEquatable<VirtualizationServer>, IComparable<VirtualizationServer>
    {
        private readonly Dictionary<Guid, Session> sessions = new Dictionary<Guid, Session>();
        private readonly Dictionary<Guid, Machine> runningMachines = new Dictionary<Guid, Machine>();
        private readonly Dictionary<MachineType, TemplateResources> templates;
        
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
        public ServerResources TotalServerResources { get; }

        /// <summary>
        /// Free resources on server
        /// </summary>
        public ServerResources FreeServerResources
        {
            get
            {
                var machines = runningMachines.Values.ToArray();
                var resources = machines.Select(machine => machine.UsingResources as Resources.Resources)
                                        .DefaultIfEmpty(new Resources.Resources(0, 0, 0))
                                        .Aggregate((resources1, resources2) => resources1 + resources2);
                var gpusUsed = machines.Select(machine => machine.UsingResources?.Gpu);
                var gpus = TotalServerResources.GpuIds.Except(gpusUsed);
                return new ServerResources(TotalServerResources - resources, gpus);
            }
        }

        /// <summary>
        /// Template resources for machine type
        /// </summary>
        public IReadOnlyDictionary<MachineType, TemplateResources> TemplateResources => templates;

        /// <summary>
        /// Machines running on server
        /// </summary>
        public IReadOnlyDictionary<Guid, Machine> RunningMachines => runningMachines;

        /// <summary>
        /// Name of RabbitMQ queue for direct communication
        /// </summary>
        public string Queue { get; }

        /// <summary>
        /// Create virtualization server with complete resources and templates
        /// </summary>
        /// <param name="totalResources">Whole resources owned by server</param>
        /// <param name="templates">Template resources for use when creating new machines</param>
        /// <param name="queue">Name of RabbitMQ queue</param>
        public VirtualizationServer(ServerResources totalResources,
                                    IDictionary<MachineType, TemplateResources> templates, string queue)
        {
            ServerGuid = Guid.NewGuid();
            TotalServerResources = totalResources;
            Queue = queue;
            this.templates = new Dictionary<MachineType, TemplateResources>(templates);
        }

        /// <summary>
        /// Create new machine of specified type with GPU
        /// </summary>
        /// <param name="type">Type of machine</param>
        /// <param name="gpuId">Identifier of GPU to use. If null - uses GPU from template resources</param>
        /// <returns>Created machine</returns>
        /// <exception cref="ArgumentException">Invalid machine type</exception>
        public Machine CreateMachine(MachineType type, GpuId gpuId = null)
        {
            var template = templates.GetValueOrDefault(type, null);
            if (template == null)
            {
                throw new ArgumentException("Invalid machine type", nameof(type));
            }

            var resources = gpuId != null
                ? new MachineResources(template, gpuId)
                : new MachineResources(template);
            var machine = new Machine(type, resources, this);
            runningMachines.Add(machine.Guid, machine);
            return machine;
        }

        /// <summary>
        /// Delete machine
        /// </summary>
        /// <param name="machineGuid">Machine identifier</param>
        public void DeleteMachine(Guid machineGuid) => runningMachines.Remove(machineGuid);

        /// <summary>
        /// Create full session on server with selected machine
        /// </summary>
        /// <param name="halfSession">Partial session (without machine)</param>
        /// <param name="machineGuid">Machine identifier</param>
        /// <returns>Session with attached machine</returns>
        /// <exception cref="ArgumentException">Session already on server or invalid guid:
        /// part of other session or doesn't exist</exception>
        public Session CreateFullSession(Session halfSession, Guid machineGuid)
        {
            if (sessions.ContainsKey(halfSession.SessionGuid))
            {
                throw new ArgumentException("This session already exists on server", nameof(halfSession));
            }
            
            if (!runningMachines.TryGetValue(machineGuid, out var machine))
            {
                throw new ArgumentException("Cannot find machine with specified guid on server", nameof(machineGuid));
            }

            if (sessions.Values.Any(session => machineGuid.Equals(session?.CorrelatedMachine?.Guid)))
            {
                throw new ArgumentException("Machine already part of session", nameof(machineGuid));
            }
            
            halfSession.AttachMachine(machine);
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