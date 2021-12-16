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
            server = PrepareVirtualizationServer();
        }

        [Test]
        public void ShouldReturnAllResourcesIfNoMachineOnServer()
        {
            Assert.AreEqual(server.TotalResources, server.AvailableResources);
        }
        
        [Test]
        public void ShouldReturnAllResourcesIfNoMachineIsRunning()
        {
            server.CreateMachine("machine1", GetGpuMachineType());
            Assert.AreEqual(server.TotalResources, server.AvailableResources);
        }
        
        [Test]
        public void ShouldReturnAllResourcesIfNoMachineIsTaken()
        {
            var machine = server.CreateMachine("machine1", GetGpuMachineType());
            machine.State = MachineState.Free;
            Assert.AreEqual(server.TotalResources, server.AvailableResources);
        }
        

        [Test]
        public void ShouldReturnResourcesWithoutRunningServers()
        {
            var machine = server.CreateMachine("machine1", GetGpuMachineType());
            machine.State = MachineState.Booting;
            
            var availableResources = server.AvailableResources;

            var expectedResources = new ServerResources(server.TotalResources - machine.UsingResources, 
                                                        server.TotalResources.GpuIds.
                                                               Except(new List<GpuId>() {machine.UsingResources.Gpu}));
            Assert.AreEqual(expectedResources, availableResources);
        }
    }
}