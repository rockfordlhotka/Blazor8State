using Blazor8State;
using Microsoft.AspNetCore.Mvc;

namespace WebApi1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StateController : ControllerBase
    {
        private readonly ILogger<StateController> _logger;
        private readonly ISessionManager _sessionList;

        public StateController(ISessionManager sessionManager, ILogger<StateController> logger)
        {
            _logger = logger;
            _sessionList = sessionManager;
        }

        [HttpGet(Name = "GetState")]
        public async Task<Blazor8State.Client.Session> Get()
        {
            var session = await _sessionList.GetSession();
            session.IsCheckedOut = true;
            return session;
        }

        [HttpPut(Name = "UpdateState")]
        public async Task Put(Blazor8State.Client.Session updatedSession)
        {
            await _sessionList.UpdateSession(updatedSession);
        }
    }
}
