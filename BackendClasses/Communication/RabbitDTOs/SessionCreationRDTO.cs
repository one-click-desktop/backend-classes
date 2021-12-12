using System;
using OneClickDesktop.BackendClasses.Model;

namespace OneClickDesktop.BackendClasses.Communication.RabbitDTOs
{
    public class SessionCreationRDTO
    {
        public Guid UserGuid { get; set; }
        public string DomainName { get; set; }
        public SessionType SessionType { get; set; }
    }
}