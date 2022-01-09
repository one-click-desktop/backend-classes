using System;

namespace OneClickDesktop.BackendClasses.Communication.RabbitDTOs
{
    /// <summary>
    /// Data for session cancel request
    /// </summary>
    public class SessionCancelRDTO
    {
        /// <summary>
        /// Session guid to cancel
        /// </summary>
        public Guid SessionGuid { get; set; }
    }
}