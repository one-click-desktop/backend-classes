using System;
using System.Collections.Generic;
using NUnit.Framework;
using OneClickDesktop.BackendClasses.Model;
using OneClickDesktop.BackendClasses.Model.Resources;

namespace OneClickDesktop.BackendClasses.ModelTests.SystemModelTests
{
    public class UpdateOrAddServer
    {
        private SystemModel model;
        
        [SetUp]
        public void SetUp()
        {
            model = new SystemModel();
        }

        [Test]
        public void ShouldAddServerIfDoesntExist()
        {
            var server = new VirtualizationServer(null, 
                                                  new Dictionary<string, TemplateResources>(),
                                                  null);
            
            model.UpdateOrAddServer(server);
            
            Assert.That(model.Servers, Contains.Item(new KeyValuePair<Guid, VirtualizationServer>(server.ServerGuid, server)));
            Assert.IsFalse(model.Servers[server.ServerGuid].Managable);
        }
        
        [Test]
        public void ShouldUpdateServerIfExists()
        {
            var server = new VirtualizationServer(null, 
                                                  new Dictionary<string, TemplateResources>(),
                                                  null);
            
            model.UpdateOrAddServer(server);
            model.UpdateOrAddServer(server);
            
            Assert.That(model.Servers, Contains.Item(new KeyValuePair<Guid, VirtualizationServer>(server.ServerGuid, server)));
            Assert.IsTrue(model.Servers[server.ServerGuid].Managable);
        }
    }
}