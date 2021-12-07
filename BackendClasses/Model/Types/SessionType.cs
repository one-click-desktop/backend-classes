using System;

namespace OneClickDesktop.BackendClasses.Model
{
    /// <summary>
    /// Tymczasowo jest to po prostu string
    /// </summary>
    public class SessionType
    {
        public string Type { get; set; }

        public override bool Equals(object obj)
        {
            return obj is MachineType type &&
                   this.Type == type.Type;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type);
        }

        public override string ToString()
        {
            return Type;
        }
    }
}