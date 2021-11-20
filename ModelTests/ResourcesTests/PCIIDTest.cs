using NUnit.Framework;
using OneClickDesktop.BackendClasses.Model.Resources;

namespace ModelTests.ResourcesTests
{
    [TestFixture]
    class PCIIDTest
    {
        [TestCase("asdfgh")]
        public void IsValidId_ToLongId_False(string id)
        {
            var res = PCIID.IsValidId(id);

            Assert.IsFalse(res);
        }

        [TestCase("asdf")]
        public void IsValidId_4CharNonHex_False(string id)
        {
            var res = PCIID.IsValidId(id);

            Assert.IsFalse(res);
        }

        [TestCase("12Ac")]
        public void IsValidId_4CharNonLowercaseHex_False(string id)
        {
            var res = PCIID.IsValidId(id);

            Assert.IsFalse(res);
        }

        [TestCase("12ac")]
        public void IsValidId_4CharLowercaseHex_True(string id)
        {
            var res = PCIID.IsValidId(id);

            Assert.IsTrue(res);
        }

        [TestCase("10de", "13c2")]
        public void ToString_FormatTest(string vendor, string device)
        {
            PCIID gm204 = new PCIID(vendor, device);

            string res = gm204.ToString();

            bool check = res == $"{vendor}:{device}";
            Assert.IsTrue(check);
        }
    }
}
