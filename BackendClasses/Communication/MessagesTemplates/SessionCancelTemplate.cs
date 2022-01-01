using System;
using OneClickDesktop.BackendClasses.Communication.RabbitDTOs;

namespace OneClickDesktop.BackendClasses.Communication.MessagesTemplates
{
    public class SessionCancelTemplate
    {
        /// <summary>
        /// Nazwa wiadmości uzywana do rozpoznania typu pakietu
        /// </summary>
        public const string MessageTypeName = "SessionCancel";
        /// <summary>
        /// Typ prznoszonej informacji - potrzebny do deserializacji obiektu przychodzącego
        /// </summary>
        //const z jakiegos powodu ne dziala! typeof() poiwnno byc w compile-time ogolnie rsolvovane, ale nie jest(?).
        public static readonly Type MessageType = typeof(SessionCancelRDTO);
    }
}