using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Top5.Contracts.DTOs;
using Top5.Business.Services;
using Top5.Domain.Entities;

namespace Top5.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class MatchesController : ControllerBase
    {
        private readonly IMatchService _matchService;
        private readonly IMapper _mapper;
        public MatchesController(IMatchService matchService,IMapper mapper)
        {
            _matchService = matchService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all matches
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Match>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Match>>> GetAllMatches()
        {
            var matches = await _matchService.GetAllAsync();
            return Ok(matches);
        }
        /// <summary>
        /// Get all matche view in application
        /// </summary>
        [HttpGet("view")]
        [ProducesResponseType(typeof(IEnumerable<MatchDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MatchDto>>> GetAllMatchesView()
        {
            var matches = await _matchService.GetAllWithTeamsAsync();
            var dto = _mapper.Map<IEnumerable<MatchDto>>(matches);
            return Ok(dto);
        }

        /// <summary>
        /// Get a match by ID
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(Match), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Match>> GetMatchById(Guid id)
        {
            var match = await _matchService.GetByIdAsync(id);
            if (match == null)
                return NotFound();

            return Ok(match);
        }

        /// <summary>
        /// Get a match by ID
        /// </summary>
        [HttpGet("{id:guid}/view")]
        [ProducesResponseType(typeof(Match), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MatchDto>> GetMatchWithTeamsById(Guid id)
        {
            var match = await _matchService.GetByIdWithTeamsAsync(id);
            if (match == null)
                return NotFound();

            var dto = _mapper.Map<MatchDto>(match);
            return Ok(dto);
        }

        /// <summary>
        /// Create a new match
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Match), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Match>> CreateMatch([FromBody] Match match)
        {
            var createdMatch = await _matchService.CreateAsync(match);
            return CreatedAtAction(nameof(GetMatchById), new { id = createdMatch.id }, createdMatch);
        }

        /// <summary>
        /// Update an existing match
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(Match), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Match>> UpdateMatch(Guid id, [FromBody] Match match)
        {
            var updatedMatch = await _matchService.UpdateAsync(id, match);
            if (updatedMatch == null)
                return NotFound();

            return Ok(updatedMatch);
        }

        /// <summary>
        /// Delete a match
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMatch(Guid id)
        {
            var deleted = await _matchService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}

