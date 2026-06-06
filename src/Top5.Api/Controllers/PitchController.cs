using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Top5.Business.Services;
using Top5.Domain.Entities;

namespace Top5.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PitchController : BaseController
    {
        private readonly IPitchService _pitchService;

        public PitchController(IPitchService pitchService)
        {
            _pitchService = pitchService;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPitches()
        {
            var result = await _pitchService.GetPitchs();
            if (!result.IsSuccess)
            {
                return Failed(result.Error ?? "An error occurred while retrieving pitches.", StatusCodes.Status500InternalServerError);
            }
            return Success(result);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddPitch([FromBody] Pitch pitch)
        {
            var result = await _pitchService.AddPitch(pitch);
            if (!result.IsSuccess)
            {
                return Failed(result.Error ?? "An error occurred while adding the pitch.", StatusCodes.Status500InternalServerError);
            }
            return Success(result);
        }


    }
}
