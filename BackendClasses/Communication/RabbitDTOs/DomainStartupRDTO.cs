using System.Text.Json.Serialization;
using OneClickDesktop.BackendClasses.Communication.MessagesTemplates;
using OneClickDesktop.BackendClasses.Model;

namespace OneClickDesktop.BackendClasses.Communication.RabbitDTOs
{
    public class DomainStartupRDTO
    {
        public string DomainName { get; set; }
        public MachineType DomainType { get; set; }
    }
}