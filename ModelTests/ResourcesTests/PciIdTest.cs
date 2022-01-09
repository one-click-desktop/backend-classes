using System;
using NUnit.Framework;
using OneClickDesktop.BackendClasses.Model.Resources;

namespace OneClickDesktop.BackendClasses.ModelTests.ResourcesTests
{
    [TestFixture]
    internal class PciIdTest
    {
        [TestCase("0000:06:12.5")]
        public void Parse_Success(string address)
        {
            var expected = new PciAddressId("0000", "06", "12", "5");
            
            var res = PciAddressId.Parse(address);

            Assert.AreEqual(expected, res);
        }
        
        [TestCase("0000:0x:12.5")]
        public void Parse_NotHex_FormatException(string address)
        {
            Assert.Throws<FormatException>(() => PciAddressId.Parse(address));
        }
        
        [TestCase("0000:022:12.5")]
        public void Parse_WrongLength_FormatException(string address)
        {
            Assert.Throws<FormatException>(() => PciAddressId.Parse(address));
        }

        [TestCase(null)] 
        public void Parse_Null_ArgumentNullException(string address)
        {
            Assert.Throws<ArgumentNullException>(() => PciAddressId.Parse(address));
        }
        
        [TestCase("0000:06:12.5")]
        public void TryParse_Success(string address)
        {
            var expected = new PciAddressId("0000", "06", "12", "5");
            
            var res = PciAddressId.TryParse(address, out var addressId);

            Assert.IsTrue(res);
            Assert.AreEqual(expected, addressId);
        }
        
        [TestCase("0000:0x:12.5")]
        public void TryParse_NotHex_Fail(string address)
        {
            var res = PciAddressId.TryParse(address, out var addressId);

            Assert.IsFalse(res);
            Assert.IsNull(addressId);
        }
        
        [TestCase("0000:022:12.5")]
        public void TryParse_WrongLength_Fail(string address)
        {
            var res = PciAddressId.TryParse(address, out var addressId);

            Assert.IsFalse(res);
            Assert.IsNull(addressId);
        }

        [TestCase(null)] 
        public void TryParse_Null_ArgumentNullException(string address)
        {
            Assert.Throws<ArgumentNullException>(() => PciAddressId.Parse(address));
        }

        [TestCase("10de", "13", "c2", "2")]
        public void ToString_FormatTest(string domain, string bus, string slot, string function)
        {
            var gm204 = new PciAddressId(domain, bus, slot, function);
            var expected = $"{domain}:{bus}:{slot}.{function}";
        
            var res = gm204.ToString();
            
            Assert.AreEqual(expected, res);
        }
        
        [TestCase("10de", "13", "c2", "2")]
        public void Equals_SameIds_Success(string domain, string bus, string slot, string function)
        {
            var id1 = new PciAddressId(domain, bus, slot, function);
            var id2 = new PciAddressId(domain, bus, slot, function);
        
        
            var check = id1.Equals(id2);
            Assert.IsTrue(check);
        }
        
        [TestCase("10de", "13", "c2", "2")]
        public void Equals_DifferentIds_Failure(string domain, string bus, string slot, string function)
        {
            var id1 = new PciAddressId(domain, bus, slot, function);
            var id2 = new PciAddressId(domain, slot, bus, function);
        
        
            var check = id1.Equals(id2);
            Assert.IsFalse(check);
        }
        
        [TestCase("10de", "13", "c2", "2")]
        public void GetHashCode_SameIds_Success(string domain, string bus, string slot, string function)
        {
            var id1 = new PciAddressId(domain, bus, slot, function);
            var id2 = new PciAddressId(domain, bus, slot, function);
        
            int hash1 = id1.GetHashCode();
            int hash2 = id2.GetHashCode();
        
            Assert.AreEqual(hash1, hash2);
        }
        
        [TestCase("10de", "13", "c2", "2")]
        public void GetHashCode_DifferentIds_Failure(string domain, string bus, string slot, string function)
        {
            var id1 = new PciAddressId(domain, bus, slot, function);
            var id2 = new PciAddressId(domain, slot, bus, function);
        
            int hash1 = id1.GetHashCode();
            int hash2 = id2.GetHashCode();
            
            Assert.AreNotEqual(hash1, hash2);
        }
    }
}
