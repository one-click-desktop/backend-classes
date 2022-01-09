using System;
using System.Collections.Generic;
using NUnit.Framework;
using OneClickDesktop.BackendClasses.Model;
using OneClickDesktop.BackendClasses.Model.Types;

namespace OneClickDesktop.BackendClasses.ModelTests.SystemModelTests
{
    [TestFixture]
    internal class CreateSession
    {
        private SystemModel model;
        
        [SetUp]
        public void SetUp()
        {
            model = new SystemModel();
        }

        [Test]
        public void ShouldReturnSessionAndAddToDictionary()
        {
            var session = model.CreateSession(null, new SessionType());
            
            Assert.That(model.Sessions, Contains.Item(new KeyValuePair<Guid, Session>(session.SessionGuid, session)));
        }
    }
}