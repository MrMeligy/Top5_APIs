using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Top5.Business.Result;
using Top5.Business.Services;
using Top5.Contracts.DTOs;
using Top5.Domain.Entities;

namespace Top5.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class MatchPlayersController : BaseController
    {
        private readonly IMatchPlayersService _matchPlayersService;

        public MatchPlayersController(IMatchPlayersService matchPlayersService)
        {
            _matchPlayersService = matchPlayersService;
        }

        [HttpGet("Match/{matchId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMatchPlayers(Guid matchId)
        {
            var response = await  _matchPlayersService.GetMatchPlayers(matchId);
            return response.IsSuccess ? Success(response.Value) : Failed(response.Error!, 400);
        }

        [HttpGet("{matchId:guid}/{playerId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPlayerStatsInMatchAsync(Guid matchId, Guid playerId)
        {
            var response = await _matchPlayersService.GetPlayerStatsInMatchAsync(matchId,playerId);
            return response.IsSuccess ? Success(response.Value) : Failed(response.Error!, 400);
        }

        [HttpGet("PlayerStats/{teamId:guid}/{playerId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPlayerTeamStatsAsync(Guid teamId, Guid playerId)
        {
            var response = await _matchPlayersService.GetPlayerTeamStatsAsync(teamId, playerId);
            return response.IsSuccess ? Success(response.Value) : Failed(response.Error!, 400);
        }

        [HttpGet("ByTeam")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPlayerMatchesByTeamAsync(Guid teamId, Guid playerId, int pageSize, int pageNumber)
        {
            var response = await _matchPlayersService.GetPlayerMatchesByTeamAsync(teamId,playerId,pageSize,pageNumber);
            return response.IsSuccess ? Success(response.Value) : Failed(response.Error!, 400);
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPlayerMatchesAsync(Guid playerId, int pageSize, int pageNumber)
        {
            var response = await _matchPlayersService.GetPlayerMatches(playerId,pageSize,pageNumber);
            return response.IsSuccess ? Success(response.Value) : Failed(response.Error!, 400);
        }

        [HttpGet("PlayerStates/{playerId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPlayerStats(Guid playerId)
        {
            var response = await _matchPlayersService.GetPlayerStats(playerId);
            return response.IsSuccess ? Success(response.Value) : Failed(response.Error!, 400);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateMatchPlayers([FromBody] CreateMatchPlayerDto dto)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdString, out var userId))
                return Failed("Not Authorized", 401);
            var response = await _matchPlayersService.CreateAsync(dto,userId);
            return response.IsSuccess ? Success(response.Value) : Failed(response.Error!, 400);
        }

    }
}

