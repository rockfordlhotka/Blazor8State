namespace Blazor8State
{
    public interface ISessionIdManager
    {
        Task<string?> GetSessionId();
    }
}
