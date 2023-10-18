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
            if (isBrowser && _sessions.Count == 0)
            {
                var client = new System.Net.Http.HttpClient();
                client.BaseAddress = new Uri("https://localhost:7095/");
                var result = await client.GetFromJsonAsync<Session>("state");
                _sessions.Add(key, result);
            }
            return _sessions[key];
        }

        public bool Contains(string key)
        { return _sessions.ContainsKey(key); }

        public void Add(string key, Session session)
        { _sessions[key] = session; }
    }
}
