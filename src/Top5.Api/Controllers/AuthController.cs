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
        public async Task<ActionResult<AuthResponseDto>> login([FromBody] AuthDto auth)
        {
            var response = await _authService.login(auth);
            if (response == null)
            {
                return StatusCode(401, new { message = "Not Authorized" });
            }
            return Ok(response);
        }
        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> register([FromBody] Player player)
        {
            var response = await _authService.register(player);
            if (response == null)
            {
                return StatusCode(401,new {message = "Not Authorized"});
            }
            return Ok(response);
        }
        [HttpPost("refresh")]
        public async Task<ActionResult<AuthResponseDto>> refresh(string token)
        {
            var response = await _authService.refresh(token);
            if (response == null)
            {
                return StatusCode(401, new { message = "Not Authorized" });
            }
            return Ok(response);
        }
        

    }
}
