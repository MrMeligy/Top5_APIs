using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Top5.Business.Services;
using Top5.Contracts.DTOs;
using Top5.Domain.Models;

namespace Top5.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> login([FromBody] AuthDto auth)
        {
            return await _authService.login(auth) ?? "wrong username or password";
        }
        [HttpPost("register")]
        public async Task<ActionResult<string>> register([FromBody] Player player)
        {
            return await _authService.register(player) ?? "registeration failed";
        }

    }
}
