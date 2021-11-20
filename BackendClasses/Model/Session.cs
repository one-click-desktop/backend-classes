using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneClickDesktop.BackendClasses.Model
{
    class Session
    {
        public Machine CorrelatedMachine { get; set; }
        public User CorrelatedUser { get; set; }
        public string SessionGuid { get; set; }

        public Session(User user, Machine machine = null)
        {
            SessionGuid = "GenerateValidRandomGuidPlease";
            CorrelatedUser = user;
            CorrelatedMachine = machine;
        }

        public Session(Session other)
        {
            SessionGuid = other.SessionGuid;
            CorrelatedUser = other.CorrelatedUser;
            CorrelatedMachine = other.CorrelatedMachine;
        }

        public void AttachMachine(Machine machine)
        {
            CorrelatedMachine = machine;
        }
    }
}
