namespace OneClickDesktop.BackendClasses.Communication.RabbitDTOs
{
    /// <summary>
    /// Data for damain shutdown request
    /// </summary>
    public class DomainShutdownRDTO
    {
        /// <summary>
        /// Domain name to shutdown
        /// </summary>
        public string DomainName { get; set; }
    }
}