using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OneClickDesktop.BackendClasses.Model.Resources;

namespace OneClickDesktop.BackendClasses.ModelTests.VirtualizationServerTests
{
    [TestFixture]
    internal class CountFreeResources : BaseVirtualizationServerTest
    {
        [SetUp]
        public void SetUp()
        {
            server = PrepareVirtualizationServer();
        }

        [Test]
        public void ShouldReturnAllResourcesIfNoMachinesRunning()
        {
            Assert.AreEqual(server.TotalServerResources, server.FreeServerResources);
        }

        [Test]
        public void ShouldReturnResourcesWithoutRunningServers()
        {
            var machine = server.CreateMachine(GetGpuMachineType());

            var freeResources = server.FreeServerResources;

            var expectedResources = new ServerResources(server.TotalServerResources - machine.UsingResources, 
                                                        server.TotalServerResources.GpuIds.
                                                               Except(new List<GpuId>() {machine.UsingResources.Gpu}));
            Assert.AreEqual(expectedResources, freeResources);
        }
    }
}
