using Microsoft.AspNetCore.Mvc;
using Top5.Business.Services;
using Top5.Contracts.DTOs;
using Top5.Domain.Models;

namespace Top5.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        /// <summary>
        /// Get all players
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Player>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Player>>> GetAllPlayers()
        {
            var players = await _playerService.GetAllAsync();
            return Ok(players);
        }
        /// <summary>
        /// Get PlayerDto
        /// </summary>
        [HttpGet("{id:guid}/view")]
        [ProducesResponseType(typeof(PlayerDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<PlayerDto>> GetPlayerDto(Guid id)
        {
            var player = await _playerService.GetPlayerDtoById(id);
            return Ok(player);
        }

        /// <summary>
        /// Get a player by ID
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(Player), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Player>> GetPlayerById(Guid id)
        {
            var player = await _playerService.GetByIdAsync(id);
            if (player == null)
                return NotFound();

            return Ok(player);
        }

        /// <summary>
        /// Create a new player
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Player), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Player>> CreatePlayer([FromBody] Player player)
        {
            var createdPlayer = await _playerService.CreateAsync(player);
            return CreatedAtAction(nameof(GetPlayerById), new { id = createdPlayer.id }, createdPlayer);
        }

        /// <summary>
        /// Update an existing player
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(Player), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Player>> UpdatePlayer(Guid id, [FromBody] Player player)
        {
            var updatedPlayer = await _playerService.UpdateAsync(id, player);
            if (updatedPlayer == null)
                return NotFound();

            return Ok(updatedPlayer);
        }

        /// <summary>
        /// Delete a player
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePlayer(Guid id)
        {
            var deleted = await _playerService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}

