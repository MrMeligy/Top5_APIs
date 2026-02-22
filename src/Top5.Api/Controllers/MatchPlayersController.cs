using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Top5.Business.Services;
using Top5.Contracts.DTOs;
using Top5.Domain.Entities;

namespace Top5.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class MatchPlayersController : ControllerBase
    {
        private readonly IMatchPlayersService _matchPlayersService;
        private readonly IMapper _mapper;

        public MatchPlayersController(IMatchPlayersService matchPlayersService,IMapper mapper)
        {
            _mapper = mapper;
            _matchPlayersService = matchPlayersService;
        }

        /// <summary>
        /// Get all match players
        /// </summary>
        [HttpGet("{id:guid}/view")]
        [ProducesResponseType(typeof(MatchPlayerDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<MatchPlayerDto>> GetByMatchPlayerViewAsync(Guid id)
        {
            var matchPlayers = await _matchPlayersService.GetByMatchPlayerViewAsync(id);
            var matchPlayersDto = _mapper.Map<MatchPlayerDto>(matchPlayers);
            return Ok(matchPlayersDto);
        }

        /// <summary>
        /// Get all match players
        /// </summary>
        [HttpGet("{playerId:guid}/PlayerView")]
        [ProducesResponseType(typeof(IEnumerable<MatchPlayerDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MatchPlayerDto>>> GetByPlayerMatchesAsync(Guid playerId)
        {
            var matchPlayers = await _matchPlayersService.GetByPlayerMatchesAsync(playerId);
            var matchPlayersDto = _mapper.Map<IEnumerable<MatchPlayerDto>>(matchPlayers);
            return Ok(matchPlayersDto);
        }

        /// <summary>
        /// Get all match players
        /// </summary>
        [HttpGet("{matchId:guid}/MatchView")]
        [ProducesResponseType(typeof(IEnumerable<MatchPlayerDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MatchPlayerDto>>> GetByMatchPlayersViewAsync(Guid matchId)
        {
            var matchPlayers = await _matchPlayersService.GetByMatchPlayersViewAsync(matchId);
            var matchPlayersDto = _mapper.Map<IEnumerable<MatchPlayerDto>>(matchPlayers);
            return Ok(matchPlayersDto);
        }

        /// <summary>
        /// Get all match players
        /// </summary>
        [HttpGet("{matchId:guid}/{playerId:guid}")]
        [ProducesResponseType(typeof(MatchPlayerDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<MatchPlayerDto>> GetMatchAndPlayerAsync(Guid matchId,Guid playerId)
        {
            var matchPlayers = await _matchPlayersService.GetMatchAndPlayerAsync(matchId,playerId);
            var matchPlayersDto = _mapper.Map<MatchPlayerDto>(matchPlayers);
            return Ok(matchPlayers);
        }
        /// <summary>
        /// Get all match players
        /// </summary>
        [HttpGet("{teamId:guid}/{playerId:guid}/playerTeamStatic")]
        [ProducesResponseType(typeof(IEnumerable<MatchPlayerDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MatchPlayerDto>>> GetPlayerTeamStatsAsync(Guid teamId, Guid playerId)
        {
            var matchPlayers = await _matchPlayersService.GetPlayerTeamStatsAsync(teamId, playerId);
            var matchPlayersDto = _mapper.Map<IEnumerable<MatchPlayerDto>>(matchPlayers);
            return Ok(matchPlayersDto);
        }

        /// <summary>
        /// Get all match players
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MatchPlayers>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MatchPlayers>>> GetAllMatchPlayers()
        {
            var matchPlayers = await _matchPlayersService.GetAllAsync();
            return Ok(matchPlayers);
        }

        /// <summary>
        /// Get a match player by ID
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(MatchPlayers), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MatchPlayers>> GetMatchPlayersById(Guid id)
        {
            var matchPlayers = await _matchPlayersService.GetByIdAsync(id);
            if (matchPlayers == null)
                return NotFound();

            return Ok(matchPlayers);
        }

        /// <summary>
        /// Create a new match player
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(MatchPlayers), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MatchPlayers>> CreateMatchPlayers([FromBody] MatchPlayers matchPlayers)
        {
            var createdMatchPlayers = await _matchPlayersService.CreateAsync(matchPlayers);
            return CreatedAtAction(nameof(GetMatchPlayersById), new { id = createdMatchPlayers.id }, createdMatchPlayers);
        }

        /// <summary>
        /// Update an existing match player
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(MatchPlayers), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MatchPlayers>> UpdateMatchPlayers(Guid id, [FromBody] MatchPlayers matchPlayers)
        {
            var updatedMatchPlayers = await _matchPlayersService.UpdateAsync(id, matchPlayers);
            if (updatedMatchPlayers == null)
                return NotFound();

            return Ok(updatedMatchPlayers);
        }

        /// <summary>
        /// Delete a match player
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMatchPlayers(Guid id)
        {
            var deleted = await _matchPlayersService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}

