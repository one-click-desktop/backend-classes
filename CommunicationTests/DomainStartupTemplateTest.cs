using System.Text.Json;
using NUnit.Framework;
using OneClickDesktop.BackendClasses.Communication.MessagesTemplates;
using OneClickDesktop.BackendClasses.Communication.RabbitDTOs;
using OneClickDesktop.BackendClasses.Model;

namespace OneClickDesktop.BackendClasses.CommunicationTests
{
    public class DomainStartupTemplateTest
    {
        [Test]
        public void JsonSerializationTest()
        {
            DomainStartupRDTO data = new DomainStartupRDTO()
            {
                DomainName = "testDomain",
                DomainType = new MachineType() {TechnicalName = "testType", HumanReadableName = "testTypeHR"}
            };

            string json = JsonSerializer.Serialize(data);
            object receivedData = JsonSerializer.Deserialize(json, DomainStartupTemplate.MessageType);
            DomainStartupRDTO res = DomainStartupTemplate.ConversionReceivedData(receivedData);
            
            Assert.IsTrue(res.DomainName == data.DomainName);
            Assert.IsTrue(res.DomainType.Equals(data.DomainType));
        }
    }
}