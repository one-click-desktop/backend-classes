using System;
using System.Collections.Generic;
using System.Linq;

namespace OneClickDesktop.BackendClasses.Model
{
    /// <summary>
    /// Describes abstract system model owned by every overseer
    /// </summary>
    class SystemModel
    {
        private Dictionary<Guid, VirtualizationServer> servers = new Dictionary<Guid, VirtualizationServer>();
        private Dictionary<Guid, Session> sessions = new Dictionary<Guid, Session>();

        /// <summary>
        /// List of virtualization servers in system
        /// </summary>
        public List<VirtualizationServer> Servers => servers.Values.ToList();
        
        /// <summary>
        /// List of sessions in system
        /// </summary>
        public List<Session> Sessions => sessions.Values.ToList();
        
        #region Sessions
        /// <summary>
        /// Get information about session by identifier
        /// </summary>
        /// <param name="sessionGuid">Session identifier</param>
        /// <returns>Session information</returns>
        public Session GetSessionInfo(Guid sessionGuid) => sessions.GetValueOrDefault(sessionGuid, null);

        /// <summary>
        /// Create new session of type for user
        /// </summary>
        /// <param name="user">Session user</param>
        /// <param name="sessionType">Session type</param>
        /// <returns>Created session</returns>
        public Session CreateSession(User user, SessionType sessionType)
        {
            var session = new Session(user, sessionType);
            sessions.Add(session.SessionGuid, session);
            return session;
        }

        /// <summary>
        /// Delete session
        /// </summary>
        /// <param name="sessionGuid">Session identifier</param>
        public void DeleteSession(Guid sessionGuid) => sessions.Remove(sessionGuid);
        #endregion
        
        #region Servers
        /// <summary>
        /// Add new virtualization server or update if it already exists
        /// </summary>
        /// <param name="server">Virtualization server to add</param>
        /// <exception cref="ArgumentNullException">Server is null</exception>
        public void UpdateOrAddServer(VirtualizationServer server)
        {
            if (server == null)
            {
                throw new ArgumentNullException(nameof(server));
            }
            servers[server.ServerGuid] = server;
        }
        #endregion
    }
}
