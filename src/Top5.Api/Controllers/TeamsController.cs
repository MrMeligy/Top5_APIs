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
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly IMapper _mapper;
        public TeamsController(ITeamService teamService,IMapper mapper)
        {
            _teamService = teamService;
            _mapper = mapper;
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
        [HttpGet("{id:guid}/view")]
        [ProducesResponseType(typeof(TeamDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TeamDto>> GetByIdViewAsync(Guid id)
        {
            var team = await _teamService.GetByIdViewAsync(id);
            if (team == null)
                return NotFound();
            var dto = _mapper.Map<TeamDto>(team);

            return Ok(dto);
        }
        /// <summary>
        /// Get all teams
        /// </summary>
        [HttpGet("view")]
        [ProducesResponseType(typeof(IEnumerable<TeamDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TeamDto>>> GetTeamsViewAsync()
        {
            var teams = await _teamService.GetTeamsViewAsync();
            var dto = _mapper.Map<IEnumerable<TeamDto>>(teams);
            return Ok(dto);
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

