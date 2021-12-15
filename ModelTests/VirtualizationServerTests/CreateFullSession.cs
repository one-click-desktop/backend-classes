using System;
using System.Collections.Generic;
using NUnit.Framework;
using OneClickDesktop.BackendClasses.Model;
using OneClickDesktop.BackendClasses.Model.States;

namespace OneClickDesktop.BackendClasses.ModelTests.VirtualizationServerTests
{
    [TestFixture]
    internal class CreateFullSession : BaseVirtualizationServerTest
    {
        [SetUp]
        public void SetUp()
        {
            server = PrepareVirtualizationServer();
        }

        [Test]
        public void ShouldAddMachineToSessionAndAddSessionToDictionary()
        {
            var session = new Session(null, new SessionType());
            var machine = server.CreateMachine("machine1", GetCpuMachineType());
            machine.State = MachineState.Free;

            var fullSession = server.CreateFullSession(session, machine.Name);
            Assert.NotNull(fullSession.CorrelatedMachine);
            Assert.AreEqual(session, fullSession);
            Assert.That(server.Sessions, Contains.Item(new KeyValuePair<Guid, Session>(session.SessionGuid, session)));
            Assert.AreEqual(SessionState.Running, fullSession.SessionState);
        }

        [Test]
        public void ShouldThrowIfMachineNotOnServer()
        {
            var session = new Session(null, new SessionType());

            var ex = Assert.Throws<ArgumentException>(() => server.CreateFullSession(session, "machine1"));
            Assert.That(ex?.ParamName, Is.EqualTo("machineName"));
            Assert.That(ex?.Message, Contains.Substring("Cannot find machine with specified guid on server"));
        }
        
        [Test]
        public void ShouldThrowIfMachinePartOfDifferentSession()
        {
            var session = new Session(null, new SessionType());
            var machine = server.CreateMachine("machine1", GetCpuMachineType());
            machine.State = MachineState.Free; 
            
            var fullSession = server.CreateFullSession(session, machine.Name);
            
            var newSession = new Session(null, new SessionType());

            var ex = Assert.Throws<ArgumentException>(() => server.CreateFullSession(newSession, machine.Name));
            Assert.That(ex?.ParamName, Is.EqualTo("machineName"));
            Assert.That(ex?.Message, Contains.Substring("Machine already part of session"));
        }
        
        [Test]
        public void ShouldThrowIfSessionExistsOnServer()
        {
            var session = new Session(null, new SessionType());
            var machine = server.CreateMachine("machine1", GetCpuMachineType());
            machine.State = MachineState.Free; 
            
            var fullSession = server.CreateFullSession(session, machine.Name);

            var ex = Assert.Throws<ArgumentException>(() => server.CreateFullSession(session, "machine2"));
            Assert.That(ex?.ParamName, Is.EqualTo("halfSession"));
            Assert.That(ex?.Message, Contains.Substring("This session already exists on server"));
        }
        
        
    }
}
