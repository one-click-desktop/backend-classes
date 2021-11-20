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
    class GPUId
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
    }
}
