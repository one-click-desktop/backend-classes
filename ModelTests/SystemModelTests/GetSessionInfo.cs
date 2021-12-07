using System;
using NUnit.Framework;
using OneClickDesktop.BackendClasses.Model;

namespace OneClickDesktop.BackendClasses.ModelTests.SystemModelTests
{
    [TestFixture]
    internal class GetSessionInfo
    {
        private SystemModel model;

        [SetUp]
        public void SetUp()
        {
            model = new SystemModel();
        }

        [Test]
        public void ShouldReturnSessionInfoWhenExists()
        {
            var session = model.CreateSession(null, new SessionType());

            var sessionInfo = model.GetSessionInfo(session.SessionGuid);

            Assert.AreEqual(session, sessionInfo);
        }

        [Test]
        public void ShouldReturnNullIfNotExists()
        {
            var sessionInfo = model.GetSessionInfo(Guid.NewGuid());

            Assert.IsNull(sessionInfo);
        }
    }
}