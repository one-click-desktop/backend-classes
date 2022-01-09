using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OneClickDesktop.BackendClasses.Model.Resources;
using OneClickDesktop.BackendClasses.Model.States;

namespace OneClickDesktop.BackendClasses.ModelTests.VirtualizationServerTests
{
    [TestFixture]
    internal class CountAvailableResources : BaseVirtualizationServerTest
    {
        [SetUp]
        public void SetUp()
        {
            Server = PrepareVirtualizationServer();
        }

        [Test]
        public void ShouldReturnAllResourcesIfNoMachineOnServer()
        {
            Assert.AreEqual(Server.TotalResources, Server.AvailableResources);
        }
        
        [Test]
        public void ShouldReturnAllResourcesIfNoMachineIsRunning()
        {
            Server.CreateMachine("machine1", GetGpuMachineType());
            Assert.AreEqual(Server.TotalResources, Server.AvailableResources);
        }
        
        [Test]
        public void ShouldReturnAllResourcesIfNoMachineIsTaken()
        {
            var machine = Server.CreateMachine("machine1", GetGpuMachineType());
            machine.State = MachineState.Free;
            Assert.AreEqual(Server.TotalResources, Server.AvailableResources);
        }
        

        [Test]
        public void ShouldReturnResourcesWithoutRunningServers()
        {
            var machine = Server.CreateMachine("machine1", GetGpuMachineType());
            machine.State = MachineState.Booting;
            
            var availableResources = Server.AvailableResources;

            var expectedResources = new ServerResources(Server.TotalResources - machine.UsingResources, 
                                                        Server.TotalResources.GpuIds.
                                                               Except(new List<GpuId>() {machine.UsingResources.Gpu}));
            Assert.AreEqual(expectedResources, availableResources);
        }
    }
}