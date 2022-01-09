using NUnit.Framework;
using OneClickDesktop.BackendClasses.Model.Types;

namespace OneClickDesktop.BackendClasses.ModelTests.TypesTests
{
    [TestFixture]
    internal class SessionTypeTests
    {
        [TestCase("cpu", "Cpu")]
        public void EqualsSessionType_Fail(string type, string wrongType)
        {
            var m1 = new SessionType() { Type = type};
            var m2 = new SessionType() { Type = wrongType};

            var check = m1.Equals(m2);

            Assert.IsFalse(check);
        }

        [TestCase("cpu", "Cpu")]
        public void EqualsSessionType_Success(string type, string wrongType)
        {
            var m1 = new SessionType() { Type = type};
            var m2 = new SessionType() { Type = type};

            var check = m1.Equals(m2);

            Assert.IsTrue(check);
        }

        [TestCase("cpu", "Cpu")]
        public void EqualsMachineType_Fail(string type, string wrongType)
        {
            var m1 = new SessionType() { Type = type };
            var m2 = new MachineType() { TechnicalName = wrongType, HumanReadableName = type };

            var check = m1.Equals(m2);

            Assert.IsFalse(check);
        }

        [TestCase("cpu", "Cpu")]
        public void EqualsMachineType_Success(string type, string wrongType)
        {
            var m1 = new SessionType() { Type = type };
            var m2 = new MachineType() { TechnicalName = type, HumanReadableName = wrongType };

            var check = m1.Equals(m2);

            Assert.IsTrue(check);
        }
    }
}