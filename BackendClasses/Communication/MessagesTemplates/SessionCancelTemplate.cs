using System;
using OneClickDesktop.BackendClasses.Communication.RabbitDTOs;

namespace OneClickDesktop.BackendClasses.Communication.MessagesTemplates
{
    /// <summary>
    /// Describes common part of session cancellation request sent from overseers to virtServers
    /// </summary>
    public class SessionCancelTemplate
    {
        /// <summary>
        /// Message name - used to recognize package type
        /// </summary>
        public const string MessageTypeName = "SessionCancel";

        /// <summary>
        /// Type of message body - used to properly cast after deserialization
        /// </summary>
        //const z jakiegos powodu ne dziala! typeof() poiwnno byc w compile-time ogolnie rsolvovane, ale nie jest(?).
        public static readonly Type MessageType = typeof(SessionCancelRDTO);

        /// <summary>
        /// Convert message body to correct type
        /// </summary>
        /// <param name="data">Message body</param>
        /// <returns>Message body as SessionCancelRDTO</returns>
        public static SessionCancelRDTO ConversionReceivedData(object data) => data as SessionCancelRDTO;
    }
}