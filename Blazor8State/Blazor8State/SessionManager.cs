using Blazor8State.Client;
using System.Threading;

namespace Blazor8State
{
    /// <summary>
    /// Dictionary containing per-user session objects, keyed
    /// by sessionId.
    /// </summary>
    public class SessionManager : ISessionManager
    { 
        private Dictionary<string, Session> _sessions = new Dictionary<string, Session>();
        private readonly ISessionIdManager _sessionIdManager;

        public SessionManager(ISessionIdManager sessionIdManager)
        {
            _sessionIdManager = sessionIdManager;
        }

        public async Task<Session> GetSession()
        {
            var key = await _sessionIdManager.GetSessionId();
            if (!_sessions.ContainsKey(key))
                _sessions.Add(key, new Session());
            var session = _sessions[key];
            // ensure session isn't checked out by wasm
            //while (session.IsCheckedOut)
            //    await Task.Delay(5);
            var endTime = DateTime.Now + TimeSpan.FromSeconds(10);
            while (session.IsCheckedOut)
            {
                if (DateTime.Now > endTime)
                    throw new TimeoutException();
                await Task.Delay(5);
            }

            return session;
        }

        public async Task UpdateSession(Session session)
        {
            if (session != null)
            {
                var key = await _sessionIdManager.GetSessionId();
                session.SessionId = key;
                Replace(session, _sessions[key]);
                _sessions[key].IsCheckedOut = false;
            }
        }

        /// <summary>
        /// Replace the contents of oldSession with the items
        /// in newSession.
        /// </summary>
        /// <param name="newSession"></param>
        /// <param name="oldSession"></param>
        private void Replace(Session newSession, Session oldSession)
        {
            oldSession.Clear();
            foreach (var key in newSession.Keys)
                oldSession.Add(key, newSession[key]);
        }
    }
}
