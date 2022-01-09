using NUnit.Framework;

namespace OneClickDesktop.BackendClasses.ModelTests.VirtualizationServerTests
{
    [TestFixture]
    internal class ShutdownMachine : BaseVirtualizationServerTest
    {
        [SetUp]
        public void SetUp()
        {
            Server = PrepareVirtualizationServer();
        }

        [Test]
        public void ShouldDeleteMachineFromDictionary()
        {
            var type = GetCpuMachineType();
            var machine = Server.CreateMachine("machine1", type);

            Server.DeleteMachine(machine.Name);
            Assert.False(Server.RunningMachines.ContainsKey(machine.Name));
        }
    }
}