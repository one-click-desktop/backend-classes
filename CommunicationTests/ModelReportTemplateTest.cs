using System.Collections.Generic;
using System.Text.Json;
using NUnit.Framework;
using OneClickDesktop.BackendClasses.Communication.MessagesTemplates;
using OneClickDesktop.BackendClasses.Model;
using OneClickDesktop.BackendClasses.Model.Resources;
using OneClickDesktop.BackendClasses.Model.States;
using OneClickDesktop.BackendClasses.Model.Types;

namespace OneClickDesktop.BackendClasses.CommunicationTests
{
    public class ModelReportTemplateTest
    {
        [Test]
        public void EmptyVirtServer()
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

        [Test]
        public void VirtSrvWithBootedMachine()
        {
            VirtualizationServer data = new VirtualizationServer(
                new ServerResources(1, 1, 1, new List<GpuId>()),
                new Dictionary<string, TemplateResources>()
                {
                    {
                        "testType",
                        new TemplateResources(
                            new MachineType() { TechnicalName = "testType", HumanReadableName = "testTypeHR" },
                            1, 1, 1, false
                        )
                    }
                },
                "queueTestDirect");
            var m = data.CreateMachine("itsAlive!",
                                       new MachineType()
                                           { TechnicalName = "testType", HumanReadableName = "testTypeHR" });
            m.AssignAddress(new MachineAddress("localhost", 1234));
            m.State = MachineState.Free;

            string json = JsonSerializer.Serialize(data);
            object receivedData = JsonSerializer.Deserialize(json, ModelReportTemplate.MessageType);
            VirtualizationServer res = ModelReportTemplate.ConversionReceivedData(receivedData);

            Assert.IsTrue(res.Equals(data));
            var mData = res.RunningMachines["itsAlive!"];
            Assert.IsTrue(mData.IpAddress?.Address == m.IpAddress?.Address);
            Assert.IsTrue(mData.IpAddress?.Port == m.IpAddress?.Port);
            Assert.IsTrue(mData.State == m.State);
        }
    }
}