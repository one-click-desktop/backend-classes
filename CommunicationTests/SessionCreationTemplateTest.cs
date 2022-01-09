using System.Text.Json;
using NUnit.Framework;
using OneClickDesktop.BackendClasses.Communication.MessagesTemplates;
using OneClickDesktop.BackendClasses.Communication.RabbitDTOs;
using OneClickDesktop.BackendClasses.Model;
using OneClickDesktop.BackendClasses.Model.Types;

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
                PartialSession = new Session(new User(), new SessionType() { Type = "testSessionType" })
            };

            string json = JsonSerializer.Serialize(data);
            object receivedData = JsonSerializer.Deserialize(json, SessionCreationTemplate.MessageType);
            SessionCreationRDTO res = SessionCreationTemplate.ConversionReceivedData(receivedData);

            Assert.IsTrue(res.DomainName == data.DomainName);
            Assert.IsTrue(res.PartialSession.SessionGuid == data.PartialSession.SessionGuid);
            Assert.IsTrue(res.PartialSession.SessionType.Equals(data.PartialSession.SessionType));
        }
    }
}