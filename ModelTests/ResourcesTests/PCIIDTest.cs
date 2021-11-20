using NUnit.Framework;
using OneClickDesktop.BackendClasses.Model.Resources;

namespace OneClickDesktop.BackendClasses.ModelTests.ResourcesTests
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

        [TestCase("10de", "13c2")]
        public void Equals_SameIds_Success(string vendor, string device)
        {
            PCIID id1 = new PCIID(vendor, device);
            PCIID id2 = new PCIID(vendor, device);


            bool check = id1.Equals(id2);
            Assert.IsTrue(check);
        }

        [TestCase("10de", "13c2")]
        public void Equals_DifferentIds_Failure(string vendor, string device)
        {
            PCIID id1 = new PCIID(vendor, device);
            PCIID id2 = new PCIID(device, vendor);


            bool check = id1.Equals(id2);
            Assert.IsFalse(check);
        }

        [TestCase("10de", "13c2")]
        public void GetHashCode_SameIds_Success(string vendor, string device)
        {
            PCIID id1 = new PCIID(vendor, device);
            PCIID id2 = new PCIID(vendor, device);

            int hash1 = id1.GetHashCode();
            int hash2 = id2.GetHashCode();

            bool check = hash1 == hash2;
            Assert.IsTrue(check);
        }

        [TestCase("10de", "13c2")]
        public void GetHashCode_DifferentIds_Failure(string vendor, string device)
        {
            PCIID id1 = new PCIID(vendor, device);
            PCIID id2 = new PCIID(device, vendor);

            int hash1 = id1.GetHashCode();
            int hash2 = id2.GetHashCode();

            bool check = hash1 == hash2;
            Assert.IsFalse(check);
        }
    }
}
