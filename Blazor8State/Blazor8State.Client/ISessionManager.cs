using Blazor8State.Client;

namespace Blazor8State
{
    public interface ISessionManager
    {
        Task<Session> GetSession();
        Task UpdateSession(Session session);
    }
}
