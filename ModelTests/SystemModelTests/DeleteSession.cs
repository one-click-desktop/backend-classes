using NUnit.Framework;
using OneClickDesktop.BackendClasses.Model;

namespace OneClickDesktop.BackendClasses.ModelTests.SystemModelTests
{
    public class DeleteSession
    {
        private SystemModel model;
        
        [SetUp]
        public void SetUp()
        {
            model = new SystemModel();
        }

        [Test]
        public void ShouldDeleteMachineFromDictionary()
        {
            var session = model.CreateSession(null, new SessionType());
            
            model.DeleteSession(session.SessionGuid);
            Assert.False(model.Sessions.ContainsKey(session.SessionGuid));
        }
    }
}