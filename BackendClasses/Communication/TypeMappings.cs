using System;
using System.Collections.Generic;
using OneClickDesktop.BackendClasses.Communication.MessagesTemplates;

namespace OneClickDesktop.BackendClasses.Communication
{
    /// <summary>
    /// Klasa zawiera mappingi nazw do typów przesyłanych wiadomości.
    /// Potrzebne do komunikacji poprzez rabbita.
    /// </summary>
    public static class TypeMappings
    {
        /// <summary>
        /// Słownik opisuje mapowanie nazw otrzymywanych waidomości przez overseera
        /// do oczekiwanych typów danych transportowanych w paczkach
        /// </summary>
        public static IReadOnlyDictionary<string, Type> OverseerReceiveMapping { get; } =
            new Dictionary<string, Type>()
            {
                { ModelReportTemplate.MessageTypeName, ModelReportTemplate.MessageType },
                { PingTemplate.MessageTypeName, PingTemplate.MessageType}
            };

        /// <summary>
        /// Słownik opisuje mapowanie nazw otrzymywanych waidomości przez virtserver
        /// do oczekiwanych typów danych transportowanych w paczkach
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