using System;
using System.Collections.Generic;
using OneClickDesktop.BackendClasses.Communication.MessagesTemplates;

namespace OneClickDesktop.BackendClasses.Communication
{
    /// <summary>
    /// Contains mapping from names to message types. Required for RabbitMQ communication
    /// </summary>
    public static class TypeMappings
    {
        /// <summary>
        /// Contains mapping from names of messages received by overseer to types of data in message body
        /// </summary>
        public static IReadOnlyDictionary<string, Type> OverseerReceiveMapping { get; } =
            new Dictionary<string, Type>()
            {
                { ModelReportTemplate.MessageTypeName, ModelReportTemplate.MessageType },
                { PingTemplate.MessageTypeName, PingTemplate.MessageType}
            };

        /// <summary>
        /// Contains mapping from names of messages received by virtServer to types of data in message body
        /// </summary>
        public static IReadOnlyDictionary<string, Type> VirtualizationServerReceiveMapping { get; } =
            new Dictionary<string, Type>()
            {
                { DomainStartupTemplate.MessageTypeName, DomainStartupTemplate.MessageType },
                { DomainShutdownTemplate.MessageTypeName, DomainShutdownTemplate.MessageType },
                { SessionCreationTemplate.MessageTypeName, SessionCreationTemplate.MessageType },
                { SessionCancelTemplate.MessageTypeName, SessionCancelTemplate.MessageType },
                { ModelReportTemplate.MessageTypeName, ModelReportTemplate.MessageType },
                { PingTemplate.MessageTypeName, PingTemplate.MessageType}
            };
    }
}