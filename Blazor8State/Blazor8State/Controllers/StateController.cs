using Blazor8State.Client;
using Microsoft.AspNetCore.Mvc;

namespace WebApi1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StateController : ControllerBase
    {
        private readonly ILogger<StateController> _logger;
        private readonly SessionManager _sessionList;
        private readonly IHttpContextAccessor _contextAccessor;

        public StateController(SessionManager sessionList, IHttpContextAccessor httpContextAccessor, ILogger<StateController> logger)
        {
            _logger = logger;
            _sessionList = sessionList;
            _contextAccessor = httpContextAccessor;
        }

        [HttpGet(Name = "GetState")]
        public async Task<Session> Get()
        {
            var httpContext = _contextAccessor.HttpContext;
            var sessionId = httpContext.Request.Cookies["sessionId"];
            var session = await _sessionList.GetSession(sessionId);
            return session;
        }
    }
}