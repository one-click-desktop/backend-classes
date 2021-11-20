using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneClickDesktop.BackendClasses.Model.States
{
    enum MachineState
    {
        TurnedOff = 0,
        Booting,
        Free,
        Reserved,
        Occupied,
        WaitingForShutdown
    }
}
