using System;
using System.Text.Json;
using NUnit.Framework;
using OneClickDesktop.BackendClasses.Communication.MessagesTemplates;
using OneClickDesktop.BackendClasses.Communication.RabbitDTOs;

namespace OneClickDesktop.BackendClasses.CommunicationTests
{
    public class SessionCancelTemplateTest
    {
        [Test]
        public void JsonSerializationTest()
        {
            SessionCancelRDTO data = new SessionCancelRDTO()
            {
                SessionGuid = Guid.NewGuid()
            };

            string json = JsonSerializer.Serialize(data);
            object receivedData = JsonSerializer.Deserialize(json, SessionCancelTemplate.MessageType);
            SessionCancelRDTO res = SessionCancelTemplate.ConversionReceivedData(receivedData);

            Assert.IsTrue(res.SessionGuid == data.SessionGuid);
        }
    }
}