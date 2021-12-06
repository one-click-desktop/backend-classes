using System;

namespace OneClickDesktop.BackendClasses.Model
{
    /// <summary>
    /// Tymczasowo jest to po prostu string
    /// </summary>
    public class SessionType
    {
        public string type;

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