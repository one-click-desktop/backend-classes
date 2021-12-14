using System;
using OneClickDesktop.BackendClasses.Model;

namespace OneClickDesktop.BackendClasses.Communication.RabbitDTOs
{
    public class SessionCreationRDTO
    {
        public Session PartialSession { get; set; }
        public string DomainName { get; set; }
    }
}