using System;
using OneClickDesktop.BackendClasses.Model;
using OneClickDesktop.BackendClasses.Model.States;

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