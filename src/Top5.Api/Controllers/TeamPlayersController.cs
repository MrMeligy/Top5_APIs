using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Top5.Business.Services;
using Top5.Contracts.DTOs;
using Top5.Domain.Entities;

namespace Top5.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TeamPlayersController : ControllerBase
    {
        private readonly ITeamPlayersService _teamPlayersService;
        private readonly IMapper _mapper;
        public TeamPlayersController(ITeamPlayersService teamPlayersService,IMapper mapper)
        {
            _teamPlayersService = teamPlayersService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all team players
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TeamPlayers>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TeamPlayers>>> GetAllTeamPlayers()
        {
            var teamPlayers = await _teamPlayersService.GetAllAsync();
            return Ok(teamPlayers);
        }

        /// <summary>
        /// Get a team player by ID
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(TeamPlayers), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TeamPlayers>> GetTeamPlayersById(Guid id)
        {
            var teamPlayers = await _teamPlayersService.GetByIdAsync(id);
            if (teamPlayers == null)
                return NotFound();

            return Ok(teamPlayers);
        }

        /// <summary>
        /// Get a team player by ID
        /// </summary>
        [HttpGet("GetTeamPlayers/{teamId:guid}")]
        [ProducesResponseType(typeof(IEnumerable<TeamPlayerDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<TeamPlayerDto>>> GetTeamPlayers(Guid teamId)
        {
            var teamPlayers = await _teamPlayersService.GetTeamPlayersAsync(teamId);
            if (teamPlayers == null)
                return NotFound();
            var dto = _mapper.Map<IEnumerable<TeamPlayerDto>>(teamPlayers);
            return Ok(dto);
        }

        /// <summary>
        /// Create a new team player
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(TeamPlayers), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TeamPlayers>> CreateTeamPlayers([FromBody] TeamPlayers teamPlayers)
        {
            var createdTeamPlayers = await _teamPlayersService.CreateAsync(teamPlayers);
            return CreatedAtAction(nameof(GetTeamPlayersById), new { id = createdTeamPlayers.id }, createdTeamPlayers);
        }

        /// <summary>
        /// Update an existing team player
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(TeamPlayers), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TeamPlayers>> UpdateTeamPlayers(Guid id, [FromBody] TeamPlayers teamPlayers)
        {
            var updatedTeamPlayers = await _teamPlayersService.UpdateAsync(id, teamPlayers);
            if (updatedTeamPlayers == null)
                return NotFound();

            return Ok(updatedTeamPlayers);
        }

        /// <summary>
        /// Delete a team player
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTeamPlayers(Guid id)
        {
            var deleted = await _teamPlayersService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}

