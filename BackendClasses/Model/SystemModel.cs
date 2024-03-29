﻿using System;
using System.Collections.Generic;
using OneClickDesktop.BackendClasses.Model.Types;

namespace OneClickDesktop.BackendClasses.Model
{
    /// <summary>
    /// Describes abstract system model owned by every overseer
    /// </summary>
    public class SystemModel
    {
        private readonly SortedDictionary<Guid, VirtualizationServer> servers = new SortedDictionary<Guid, VirtualizationServer>();
        private readonly Dictionary<Guid, Session> sessions = new Dictionary<Guid, Session>();

        /// <summary>L
        /// Virtualization servers in system
        /// </summary>
        public IReadOnlyDictionary<Guid, VirtualizationServer> Servers => servers;

        /// <summary>
        /// Sessions in system
        /// </summary>
        public IReadOnlyDictionary<Guid, Session> Sessions => sessions;
        
        #region Sessions
        /// <summary>
        /// Get information about session by identifier
        /// </summary>
        /// <param name="sessionGuid">Session identifier</param>
        /// <returns>Session information</returns>
        public Session GetSessionInfo(Guid sessionGuid) => sessions.GetValueOrDefault(sessionGuid, null);

        /// <summary>
        /// Create new session of Type for user
        /// </summary>
        /// <param name="user">Session user</param>
        /// <param name="sessionType">Session Type</param>
        /// <returns>Created session</returns>
        public Session CreateSession(User user, SessionType sessionType)
        {
            var session = new Session(user, sessionType);
            sessions.Add(session.SessionGuid, session);
            return session;
        }
        
        /// <summary>
        /// Update session or add if didn't exist
        /// </summary>
        /// <param name="session">Session to update</param>
        /// <exception cref="ArgumentNullException">Session is null</exception>
        public void UpdateOrAddSession(Session session)
        {
            if (session == null)
            {
                throw new ArgumentNullException(nameof(session));
            }
            sessions[session.SessionGuid] = session;
        }

        /// <summary>
        /// Delete session
        /// </summary>
        /// <param name="sessionGuid">Session identifier</param>
        public void DeleteSession(Guid sessionGuid) => sessions.Remove(sessionGuid);

        /// <summary>
        /// Delete multiple sessions
        /// </summary>
        /// <param name="sessionGuids">Sessions identifiers</param>
        public void DeleteSession(IEnumerable<Guid> sessionGuids)
        {
            foreach(Guid guid in sessionGuids)
                DeleteSession(guid);
        }
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

            var managable = servers.ContainsKey(server.ServerGuid);
            server.Managable = managable;
            servers[server.ServerGuid] = server;
        }
        
        /// <summary>
        /// Delete server
        /// </summary>
        /// <param name="serverGuid">Server identifier</param>
        public void DeleteServer(Guid serverGuid) => servers.Remove(serverGuid);
        #endregion
    }
}
