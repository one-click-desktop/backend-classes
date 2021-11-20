using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneClickDesktop.BackendClasses.Model.Resources
{

    /// <summary>
    /// Class describing GPU as collection of PCI identifiers
    /// </summary>
    public class GPUId
    {
        public List<PCIID> PCIIdentifiers { get; set; }

        public GPUId(IEnumerable<PCIID> identifiers)
        {
            PCIIdentifiers = new List<PCIID>(identifiers);
        }

        public override string ToString()
        {
            return String.Join(",", PCIIdentifiers);
        }

        public override bool Equals(object obj)
        {
            //To do poprawki i sprawdzenia w testach
            return obj is GPUId id &&
                   EqualityComparer<List<PCIID>>.Default.Equals(PCIIdentifiers, id.PCIIdentifiers);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PCIIdentifiers);
        }
    }
}
