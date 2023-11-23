using System.Net.Http.Json;

namespace Blazor8State.Client
{
    /// <summary>
    /// Dictionary containing per-user session objects, keyed
    /// by sessionId.
    /// </summary>
    public class SessionManager
    { 
        private Dictionary<string, Session> _sessions = new Dictionary<string, Session>();

        public async Task<Session> GetSession(string key)
        {
            var isBrowser = (System.Environment.OSVersion.Platform == PlatformID.Other);
            if (isBrowser)
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7095/");
                var result = await client.GetFromJsonAsync<Session>("state");
                if (_sessions.Count > 0)
                    Replace(result, _sessions[key]);
                else
                   _sessions[key] = result;
            }
            else
            {
                // ensure value isn't checked out
                var session = _sessions[key];
                while (session.IsCheckedOut) 
                    await Task.Delay(5);
            }
            return _sessions[key];
        }

        public async Task<Session?> UpdateSession(string key, Session session)
        {
            Session? result = null;
            if (session != null)
            {
                session.SessionId = key;
                var isBrowser = (System.Environment.OSVersion.Platform == PlatformID.Other);
                if (isBrowser)
                {
                    var client = new HttpClient();
                    client.BaseAddress = new Uri("https://localhost:7095/");
                    await client.PutAsJsonAsync<Session>("state", session);
                    result = session;
                }
                else
                {
                    Replace(session, _sessions[key]);
                    result = _sessions[key];
                    session.IsCheckedOut = false;
                    result.IsCheckedOut = false;
                }
            }
            return result;
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

        public bool Contains(string key)
        { return _sessions.ContainsKey(key); }

        public void Add(string key, Session session)
        {
            session.SessionId = key;           
            _sessions[key] = session; 
        }
    }
}
