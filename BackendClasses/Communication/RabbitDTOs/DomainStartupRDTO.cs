using OneClickDesktop.BackendClasses.Model.Types;

namespace OneClickDesktop.BackendClasses.Communication.RabbitDTOs
{
    /// <summary>
    /// Data for domain startup request
    /// </summary>
    public class DomainStartupRDTO
    {
        /// <summary>
        /// Domain name to start
        /// </summary>
        public string DomainName { get; set; }
        /// <summary>
        /// Domain type to start
        /// </summary>
        public MachineType DomainType { get; set; }
    }
}