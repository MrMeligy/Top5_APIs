using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Top5.Api.Helper;
using Top5.Business.Result;
using Top5.Business.Services;
using Top5.Contracts.DTOs;
using Top5.Domain.Models;

namespace Top5.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] AuthDto auth)
        {
            var response = await _authService.login(auth);
            if (!response.IsSuccess)
            {
                return Failed(response.Error!,401);
            }
            return Success(response.Value);
        }
        [HttpPost("register")]
        public async Task<IActionResult> register([FromBody] Player player)
        {
            var response = await _authService.register(player);
            if (!response.IsSuccess)
            {
                return Failed(response.Error!,401);
            }
            return Created(response.Value);
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> refresh([FromBody]RefreshTokenDto tokenDto)
        {
            var response = await _authService.refresh(tokenDto.token);
            if (!response.IsSuccess)
            {
                return Failed(response.Error!,401);
            }
            return Created(response.Value);
        }
        

    }
}
