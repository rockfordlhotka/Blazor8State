
using Microsoft.JSInterop;

namespace Blazor8State.Client
{
    public class SessionIdManager : ISessionIdManager
    {
        private readonly IJSRuntime JsRuntime;

        public SessionIdManager(IJSRuntime jsRuntime) 
        {
            JsRuntime = jsRuntime;
        }

        public async Task<string?> GetSessionId()
        {
            var result = await JsRuntime.InvokeAsync<string>("ReadCookie.ReadCookie", "sessionId");
            return result;
        }
    }
}
