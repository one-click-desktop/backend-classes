using System.Text.Json;
using NUnit.Framework;
using OneClickDesktop.BackendClasses.Communication.MessagesTemplates;
using OneClickDesktop.BackendClasses.Communication.RabbitDTOs;

namespace OneClickDesktop.BackendClasses.CommunicationTests
{
    public class DomainShutdownTemplateTest
    {
        [Test]
        public void JsonSerializationTest()
        {
            DomainShutdownRDTO data = new DomainShutdownRDTO()
            {
                DomainName = "testDomainName"
            };

            string json = JsonSerializer.Serialize(data);
            object receivedData = JsonSerializer.Deserialize(json, DomainShutdownTemplate.MessageType);
            DomainShutdownRDTO res = DomainShutdownTemplate.ConversionReceivedData(receivedData);
            
            Assert.IsTrue(res.DomainName == data.DomainName);
        }
    }
}