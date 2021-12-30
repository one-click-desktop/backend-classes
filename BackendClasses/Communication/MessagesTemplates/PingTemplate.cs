using System;
using OneClickDesktop.BackendClasses.Model;

namespace OneClickDesktop.BackendClasses.Communication.MessagesTemplates
{
    /// <summary>
    /// Klasa opisuje wspólną część pustej wiadomości.
    /// Po otrzymaniu tej wiadomości powinna zostac zdjęta z kolejki oraz zignorowana.
    /// </summary>
    public class PingTemplate
    {
        /// <summary>
        /// Nazwa wiadmości uzywana do rozpoznania typu pakietu
        /// </summary>
        public const string MessageTypeName = "Ping";
        /// <summary>
        /// Typ prznoszonej informacji - potrzebny do deserializacji obiektu przychodzącego
        /// </summary>
        public static readonly Type MessageType = typeof(object);
    }
}