using Microsoft.AspNetCore.Mvc;
using Top5.Business.Services;
using Top5.Domain.Entities;

namespace Top5.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamsController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        /// <summary>
        /// Get all teams
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Team>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Team>>> GetAllTeams()
        {
            var teams = await _teamService.GetAllAsync();
            return Ok(teams);
        }

        /// <summary>
        /// Get a team by ID
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(Team), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Team>> GetTeamById(Guid id)
        {
            var team = await _teamService.GetByIdAsync(id);
            if (team == null)
                return NotFound();

            return Ok(team);
        }

        /// <summary>
        /// Create a new team
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Team), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Team>> CreateTeam([FromBody] Team team)
        {
            var createdTeam = await _teamService.CreateAsync(team);
            return CreatedAtAction(nameof(GetTeamById), new { id = createdTeam.id }, createdTeam);
        }

        /// <summary>
        /// Update an existing team
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(Team), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Team>> UpdateTeam(Guid id, [FromBody] Team team)
        {
            var updatedTeam = await _teamService.UpdateAsync(id, team);
            if (updatedTeam == null)
                return NotFound();

            return Ok(updatedTeam);
        }

        /// <summary>
        /// Delete a team
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTeam(Guid id)
        {
            var deleted = await _teamService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}

