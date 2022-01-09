using NUnit.Framework;
using OneClickDesktop.BackendClasses.Model.Types;

namespace OneClickDesktop.BackendClasses.ModelTests.TypesTests
{
    [TestFixture]
    internal class MachineTypeTest
    {
        [TestCase("cpu", "Cpu")]
        public void EqualsMachineType_Fail(string technicalName, string readableName)
        {
            var m1 = new MachineType() { TechnicalName = technicalName, HumanReadableName = readableName };
            var m2 = new MachineType() { TechnicalName = readableName, HumanReadableName = technicalName };

            var check = m1.Equals(m2);

            Assert.IsFalse(check);
        }

        [TestCase("cpu", "Cpu")]
        public void EqualsMachineType_Success(string technicalName, string readableName)
        {
            var m1 = new MachineType() { TechnicalName = technicalName, HumanReadableName = readableName };
            var m2 = new MachineType() { TechnicalName = technicalName, HumanReadableName = readableName };

            var check = m1.Equals(m2);

            Assert.IsTrue(check);
        }

        [TestCase("cpu", "Cpu")]
        public void EqualsSessionType_Fail(string technicalName, string readableName)
        {
            var m1 = new MachineType() { TechnicalName = technicalName, HumanReadableName = readableName };
            var m2 = new SessionType() { Type = readableName };

            var check = m1.Equals(m2);

            Assert.IsFalse(check);
        }

        [TestCase("cpu", "Cpu")]
        public void EqualsSessionType_Success(string technicalName, string readableName)
        {
            var m1 = new MachineType() { TechnicalName = technicalName, HumanReadableName = readableName };
            var m2 = new SessionType() { Type = technicalName };

            var check = m1.Equals(m2);

            Assert.IsTrue(check);
        }

        [TestCase("cpu", "Cpu")]
        public void EqualsString_Fail(string technicalName, string readableName)
        {
            var m1 = new MachineType() { TechnicalName = technicalName, HumanReadableName = readableName };
            var m2 = readableName;

            var check = m1.Equals(m2);

            Assert.IsFalse(check);
        }

        [TestCase("cpu", "Cpu")]
        public void EqualsString_Success(string technicalName, string readableName)
        {
            var m1 = new MachineType() { TechnicalName = technicalName, HumanReadableName = readableName };
            var m2 = technicalName;

            var check = m1.Equals(m2);

            Assert.IsTrue(check);
        }
    }
}