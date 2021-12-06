using System;
using System.Collections.Generic;
using System.Linq;
using OneClickDesktop.BackendClasses.Model.Resources;

namespace OneClickDesktop.BackendClasses.Model
{
    /// <summary>
    /// Represents virtualization server
    /// </summary>
    public class VirtualizationServer: IEquatable<VirtualizationServer>, IComparable<VirtualizationServer>
    {
        /// <summary>
        /// Server identifier
        /// </summary>
        public Guid ServerGuid { get; }
        
        /// <summary>
        /// Sessions running on server
        /// </summary>
        public Dictionary<Guid, Session> SessionsOnServer { get; } = new Dictionary<Guid, Session>();
        
        /// <summary>
        /// Complete resources owned by server
        /// </summary>
        public ServerResources WholeServerResources { get; }

        /// <summary>
        /// Free resources on server
        /// </summary>
        public ServerResources FreeServerResources
        {
            get
            {
                var machines = RunningMachines.Values;
                var resources = machines.Select(machine => machine.UsingResources as Resources.Resources)
                                        .Aggregate((resources1, resources2) =>
                                        {
                                            return new Resources.Resources(resources1.Memory + resources2.Memory,
                                                                           resources1.CpuCores +
                                                                           resources2.CpuCores,
                                                                           resources1.Storage + resources2.Storage);
                                        });
                var gpusUsed = machines.Select(machine => machine.UsingResources?.Gpu);
                var gpus = WholeServerResources.GpuIds.Except(gpusUsed);
                return new ServerResources(resources, gpus);
            }
        }

        /// <summary>
        /// Template resources for machine type
        /// </summary>
        public Dictionary<MachineType, TemplateResources> TemplateResources { get; }

        /// <summary>
        /// Machines running on server
        /// </summary>
        public Dictionary<Guid, Machine> RunningMachines { get; } = new Dictionary<Guid, Machine>();
        
        /// <summary>
        /// Create virtualization server with complete resources and templates
        /// </summary>
        /// <param name="wholeResources">Whole resources owned by server</param>
        /// <param name="templates">Template resources for use when creating new machines</param>
        public VirtualizationServer(ServerResources wholeResources, Dictionary<MachineType, TemplateResources> templates)
        {
            ServerGuid = Guid.NewGuid();
            WholeServerResources = wholeResources;
            TemplateResources = templates;
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
            var template = TemplateResources.GetValueOrDefault(type, null);
            if (template == null)
            {
                throw new ArgumentException("Invalid machine type", nameof(type));
            }
            
            var resources = gpuId != null
                ? new MachineResources(template, gpuId)
                : new MachineResources(template);
            var machine = new Machine(type, resources, this);
            RunningMachines.Add(machine.Guid, machine);
            return machine;
        }

        /// <summary>
        /// Delete machine
        /// </summary>
        /// <param name="machineGuid">Machine identifier</param>
        public void DeleteMachine(Guid machineGuid) => RunningMachines.Remove(machineGuid);

        /// <summary>
        /// Create full session on server with selected machine
        /// </summary>
        /// <param name="halfSession">Partial session (without machine)</param>
        /// <param name="machineGuid">Machine identifier</param>
        /// <returns></returns>
        public Session CreateFullSession(Session halfSession, Guid machineGuid)
        {
            halfSession.AttachMachine(RunningMachines[machineGuid]);
            SessionsOnServer.Add(halfSession.SessionGuid, halfSession);
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
