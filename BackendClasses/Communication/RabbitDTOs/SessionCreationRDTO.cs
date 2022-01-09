using OneClickDesktop.BackendClasses.Model;

namespace OneClickDesktop.BackendClasses.Communication.RabbitDTOs
{
    /// <summary>
    /// Data for session creation request
    /// </summary>
    public class SessionCreationRDTO
    {
        /// <summary>
        /// Partially created session
        /// </summary>
        public Session PartialSession { get; set; }
        /// <summary>
        /// Machine wished to attach to session
        /// </summary>
        public string DomainName { get; set; }
    }
}