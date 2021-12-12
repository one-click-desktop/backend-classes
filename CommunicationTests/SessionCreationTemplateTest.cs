using System;
using System.Text.Json;
using NUnit.Framework;
using OneClickDesktop.BackendClasses.Communication.MessagesTemplates;
using OneClickDesktop.BackendClasses.Communication.RabbitDTOs;
using OneClickDesktop.BackendClasses.Model;

namespace OneClickDesktop.BackendClasses.CommunicationTests
{
    public class SessionCreationTemplateTest
    {
        [Test]
        public void JsonSerializationTest()
        {
            SessionCreationRDTO data = new SessionCreationRDTO()
            {
                DomainName = "testDomainName",
                SessionType = new  SessionType() { Type = "testSessionType"},
                UserGuid = new Guid()
            };

            string json = JsonSerializer.Serialize(data);
            object receivedData = JsonSerializer.Deserialize(json, SessionCreationTemplate.MessageType);
            SessionCreationRDTO res = SessionCreationTemplate.ConversionReceivedData(receivedData);
            
            Assert.IsTrue(res.DomainName == data.DomainName);
            Assert.IsTrue(res.UserGuid == data.UserGuid);
            Assert.IsTrue(res.SessionType.Equals(data.SessionType));
        }
    }
}