using NUnit.Framework;
using OneClickDesktop.BackendClasses.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneClickDesktop.BackendClasses.ModelTests.VirtualisationServerTests
{
    [TestFixture]
    class CreateMachine: BaseVirtualisationServerTest
    {
        [SetUp]
        public void SetUp()
        {
            virtsrv = PrepareVirtualisationServer();
        }

        public void BootUnknownType_Failure()
        {
            string name = "Uknown_type_machine";
            MachineType type = GetUnknownMachineType();


        }
    }
}
