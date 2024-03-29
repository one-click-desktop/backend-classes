﻿using System;
using System.Collections.Generic;
using NUnit.Framework;
using OneClickDesktop.BackendClasses.Model;

namespace OneClickDesktop.BackendClasses.ModelTests.VirtualizationServerTests
{
    [TestFixture]
    internal class CreateMachine: BaseVirtualizationServerTest
    {
        [SetUp]
        public void SetUp()
        {
            Server = PrepareVirtualizationServer();
        }

        [Test]
        public void ShouldThrowInvalidMachineType()
        {
            var type = GetUnknownMachineType();

            var ex = Assert.Throws<ArgumentException>(() => Server.CreateMachine("machine1", type));
            Assert.That(ex?.ParamName, Is.EqualTo("type"));
            Assert.That(ex?.Message, Contains.Substring("Invalid machine Type"));
        }

        [Test]
        public void ShouldCreateMachineWithTemplateResourcesAndAddToDictionary()
        {
            var type = GetCpuMachineType();

            var machine = Server.CreateMachine("machine1", type);
            Assert.AreEqual(Server.TemplateResources[type.TechnicalName], machine.UsingResources);
            Assert.That(Server.RunningMachines, Contains.Item(new KeyValuePair<string, Machine>(machine.Name, machine)));
        }
    }
}
