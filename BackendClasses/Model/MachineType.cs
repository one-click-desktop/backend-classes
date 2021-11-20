using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneClickDesktop.BackendClasses.Model
{
    /// <summary>
    /// Tymczasowo jest to po prostu string
    /// </summary>
    public class MachineType
    {
        string type;

        public override bool Equals(object obj)
        {
            return obj is MachineType type &&
                   this.type == type.type;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(type);
        }
    }
}
