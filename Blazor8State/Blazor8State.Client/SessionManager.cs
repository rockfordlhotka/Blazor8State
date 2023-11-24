using System.Net.Http.Json;

namespace Blazor8State.Client
{
    /// <summary>
    /// Dictionary containing per-user session objects, keyed
    /// by sessionId.
    /// </summary>
    public class SessionManager : ISessionManager
    {
        private Session _session;

        public async Task<Session> GetSession()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7095/");
            var result = await client.GetFromJsonAsync<Session>("state");
            _session = result;
            return _session;
        }

        public async Task UpdateSession(Session session)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7095/");
            await client.PutAsJsonAsync<Session>("state", session);
            _session = session;
        }
    }
}
