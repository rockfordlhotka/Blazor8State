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
            return _sessions[key];
        }

        public async Task UpdateSession(string key, Session session)
        {
            if (session == null) return;
            var isBrowser = (System.Environment.OSVersion.Platform == PlatformID.Other);
            if (isBrowser)
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7095/");
                await client.PutAsJsonAsync<Session>("state", session);
            }
            else
            {
                Replace(session, _sessions[key]);
            }
        }

        private void Replace(Session newSession, Session oldSession)
        {
            oldSession.Clear();
            foreach (var key in newSession.Keys)
                oldSession.Add(key, newSession[key]);
            oldSession.OnChanged();
        }

        public bool Contains(string key)
        { return _sessions.ContainsKey(key); }

        public void Add(string key, Session session)
        { _sessions[key] = session; }
    }
}
