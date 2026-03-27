using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Top5.Business.Services;
using Top5.Contracts.DTOs;
using Top5.Domain.Entities;

namespace Top5.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class TeamPlayersController : BaseController
    {
        private readonly ITeamPlayersService _teamPlayersService;
        public TeamPlayersController(ITeamPlayersService teamPlayersService)
        {
            _teamPlayersService = teamPlayersService;
        }

        [HttpGet("{playerId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPlayerTeams(Guid playerId)
        {
            var response = await _teamPlayersService.GetPlayerTeams(playerId);
            return response.IsSuccess ? Success(response.Value) : Failed(response.Error!, 400);
        }
        [HttpGet("team/{teamId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTeamPlayers(Guid teamId)
        {
            var response = await _teamPlayersService.GetTeamPlayers(teamId);
            return response.IsSuccess ? Success(response.Value) : Failed(response.Error!, 400);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateTeamPlayers(CreateTeamPlayerDto dto)
        {
            var response = await _teamPlayersService.CreateAsync(dto);
            return response.IsSuccess ? Success(response.Value) : Failed(response.Error!, 400);
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ExitTeam(Guid teamId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var id = Guid.Parse(userId!);
            var response = await _teamPlayersService.ExitFromTeam(teamId,id);
            return response!.IsSuccess ? Success(response.Value) : Failed(response.Error!, 400);
        }

    }
}

