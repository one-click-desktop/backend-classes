using System.Collections.Generic;
using System.Text.Json;
using NUnit.Framework;
using OneClickDesktop.BackendClasses.Communication.MessagesTemplates;
using OneClickDesktop.BackendClasses.Model;
using OneClickDesktop.BackendClasses.Model.Resources;

namespace OneClickDesktop.BackendClasses.CommunicationTests
{
    public class ModelReportTemplateTest
    {
        [Test]
        public void JsonSerializationTest()
        {
            VirtualizationServer data = new VirtualizationServer(
                new ServerResources(1, 1, 1, new List<GpuId>()),
                new Dictionary<string, TemplateResources>(),
                "queueTestDirect");

            string json = JsonSerializer.Serialize(data);
            object receivedData = JsonSerializer.Deserialize(json, ModelReportTemplate.MessageType);
            VirtualizationServer res = ModelReportTemplate.ConversionReceivedData(receivedData);
            
            Assert.IsTrue(res.Equals(data));
        }
    }
}