using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Top5.Business.Services;
using Top5.Contracts.DTOs;
using Top5.Contracts.Helper;
using Top5.Domain.Entities;

namespace Top5.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class TeamsController : BaseController
    {
        private readonly ITeamService _teamService;
        private readonly IMapper _mapper;
        public TeamsController(ITeamService teamService,IMapper mapper)
        {
            _teamService = teamService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a team by ID
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdViewAsync(Guid id)
        {
            var response = await _teamService.GetByIdViewAsync(id);
            if (!response.IsSuccess)
                return Failed(response.Error!,404);
            var dto = _mapper.Map<TeamDto>(response.Value);
            return Success(dto);
        }
        /// <summary>
        /// Get all teams
        /// </summary>
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchTeamsAsync(string name, int pageNumber,int pageSize)
        {
            var paginationDto = new PaginationDto { pageNumber = pageNumber, pageSize = pageSize };
            var response = await _teamService.SearchTeam(paginationDto,name);
            if (!response.IsSuccess)
                return Failed(response.Error!,404);
            var dto = _mapper.Map<PaginationResponse<TeamDto>>(response.Value);
            return Success(dto); 
        }
        [HttpGet("/standings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStandings(int pageNumber,int pageSize)
        {
            var paginationDto = new PaginationDto { pageNumber = pageNumber, pageSize = pageSize };
            var response = await _teamService.GetLeaderBoard(paginationDto);
            if (!response.IsSuccess)
                return Failed(response.Error!,404);
            var dto = _mapper.Map<PaginationResponse<TeamDto>>(response.Value);
            return Success(dto); 
        }

        /// <summary>
        /// Create a new team
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTeam([FromBody] CreateTeamDto team)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var id = Guid.Parse(userId!);
            var response = await _teamService.CreateAsync(team,id);
            return response.IsSuccess ? Success(response.Value) : Failed(response.Error!, 400);
        }

        /// <summary>
        /// Update an existing team
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTeam(Guid id, [FromBody] UpdateTeamInfoDto team)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var capId = Guid.Parse(userId!);
            var response = await _teamService.UpdateInfoAsync(id,capId,team);

            return response.IsSuccess ? Success(response.Value) : Failed(response.Error!, 400);
        }

        //for updating stats only by admin
        //[HttpPut("/stats")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> UpdateTeamStats(Guid id, [FromBody] UpdateTeamStatsDto team)
        //{
        //    var response = await _teamService.UpdateStatsAsync(id, team);

        //    return response.IsSuccess ? Success(response.Value) : Failed(response.Error!, 400);
        //}
        /// <summary>
        /// Delete a team
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTeam(Guid id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var capId = Guid.Parse(userId!);
            var response = await _teamService.DeleteAsync(id,capId);

            return response.IsSuccess ? Success(response.Value) : Failed(response.Error!, 400);
        }
    }
}

