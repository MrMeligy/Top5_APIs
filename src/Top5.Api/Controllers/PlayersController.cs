using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Top5.Business.Services;
using Top5.Contracts.DTOs;
using Top5.Domain.Models;

namespace Top5.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class PlayersController : BaseController
    {
        private readonly IPlayerService _playerService;

        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPlayerDto(Guid id)
        {
            var response = await _playerService.GetPlayerDtoById(id);
            return response.IsSuccess ? Success(response.Value) : Failed(response.Error!,400);

        }
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchPlayers(string userName)
        {
            var response = await _playerService.SearchPlayersAsync(userName);
            return response.IsSuccess ? Success(response.Value) : Failed(response.Error!,400);

        }
        
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(Player), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePlayer(Guid id, [FromBody] Player player)
        {
            var response = await _playerService.UpdateAsync(id, player);
            
            return response.IsSuccess ? Success(response.Value) : Failed(response.Error!, 400);
        }


        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePlayer(Guid id)
        {
            var response = await _playerService.DeleteAsync(id);
            
            return response.IsSuccess ? Success(response.Value) : Failed(response.Error!, 400);
        }
    }
}

