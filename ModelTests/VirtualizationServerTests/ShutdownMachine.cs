﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace OneClickDesktop.BackendClasses.ModelTests.VirtualizationServerTests
{
    [TestFixture]
    internal class ShutdownMachine : BaseVirtualizationServerTest
    {
        [SetUp]
        public void SetUp()
        {
            server = PrepareVirtualizationServer();
        }

        [Test]
        public void ShouldDeleteMachineFromDictionary()
        {
            var type = GetCpuMachineType();
            var machine = server.CreateMachine(type);
            
            server.DeleteMachine(machine.Guid);
            Assert.False(server.RunningMachines.ContainsKey(machine.Guid));
        }
    }
}