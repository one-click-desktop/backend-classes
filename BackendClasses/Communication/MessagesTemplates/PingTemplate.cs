using System;

namespace OneClickDesktop.BackendClasses.Communication.MessagesTemplates
{
    /// <summary>
    /// Describes common part of machine shutdown request sent from overseers to virtServers
    /// </summary>
    public class PingTemplate
    {
        /// <summary>
        /// Message name - used to recognize package type
        /// </summary>
        public const string MessageTypeName = "Ping";

        /// <summary>
        /// Type of message body - used to properly cast after deserialization
        /// </summary>
        public static readonly Type MessageType = typeof(object);
    }
}