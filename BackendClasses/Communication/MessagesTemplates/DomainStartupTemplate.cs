using System;
using OneClickDesktop.BackendClasses.Communication.RabbitDTOs;

namespace OneClickDesktop.BackendClasses.Communication.MessagesTemplates
{
    /// <summary>
    /// Klasa opisuje wspólną część prośby o włączenie maszyny przesyłanej pomiędzy overseerami i virtserverami.
    /// </summary>
    public class DomainStartupTemplate
    {
        /// <summary>
        /// Nazwa wiadmości uzywana do rozpoznania typu pakietu
        /// </summary>
        public const string MessageTypeName = "DomainStartup";
        /// <summary>
        /// Typ prznoszonej informacji - potrzebny do deserializacji obiektu przychodzącego
        /// </summary>
        //const z jakiegos powodu ne dziala! typeof() poiwnno byc w compile-time ogolnie rsolvovane, ale nie jest(?).
        public static readonly Type MessageType = typeof(DomainStartupRDTO);

        public static DomainStartupRDTO ConversionReceivedData(object data) => data as DomainStartupRDTO;
    }
}