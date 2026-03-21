using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Top5.Business.Result;
using Top5.Business.Services;
using Top5.Contracts.DTOs;
using Top5.Domain.Entities;
using Top5.Domain.Enums;

namespace Top5.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class MatchesController : BaseController
    {
        private readonly IMatchService _matchService;
        private readonly IMapper _mapper;
        public MatchesController(IMatchService matchService, IMapper mapper)
        {
            _matchService = matchService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all matches
        /// </summary>
        [HttpGet("AllMatches/{teamId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllMatches(Guid teamId, int pageSize,int pageNumber)
        {
            var response = await _matchService.GetAllTeamMatches(teamId,new PaginationDto { pageNumber = pageNumber,pageSize=pageSize});
            return response.IsSuccess ? Success(response.Value) : Failed(response.Error!, 400);
        }
        /// <summary>
        /// Get all matche view in application
        /// </summary>
        [HttpGet("History/{teamId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMatchesHistory(Guid teamId, int pageSize, int pageNumber)
        {
            var response = await _matchService.GetMatchesHistory(teamId, new PaginationDto { pageNumber = pageNumber, pageSize = pageSize });
            return response.IsSuccess ? Success(response.Value) : Failed(response.Error!, 400);
        }

        /// <summary>
        /// Get a match by ID
        /// </summary>
        [HttpGet("Schedule/{teamId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMatchById(Guid teamId, int pageSize, int pageNumber)
        {
            var response = await _matchService.GetTeamSchedule(teamId, new PaginationDto { pageNumber = pageNumber, pageSize = pageSize });
            return response.IsSuccess ? Success(response.Value) : Failed(response.Error!, 400);
        }

        /// <summary>
        /// Get a match by ID
        /// </summary>
        [HttpGet("Rejected/{teamId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRejectedMatches(Guid teamId, MatchStatues status ,int pageSize, int pageNumber)
        {
            var response = await _matchService.GetMatchesByStatus(teamId,status, new PaginationDto { pageNumber = pageNumber, pageSize = pageSize });
            return response.IsSuccess ? Success(response.Value) : Failed(response.Error!, 400);
        }
        [HttpGet("Date/{teamId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMatchesByDate(Guid teamId,DateOnly date)
        {
            var response = await _matchService.GetMatchesByDate(teamId,date);
            return response.IsSuccess ? Success(response.Value) : Failed(response.Error!, 400);
        }
        [HttpGet("PendingSent/{teamId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPendingMatchesSentAsync(Guid teamId, int pageSize, int pageNumber)
        {
            var response = await _matchService.GetPendingMatchesSent(teamId, new PaginationDto { pageNumber = pageNumber, pageSize = pageSize });
            return response.IsSuccess ? Success(response.Value) : Failed(response.Error!, 400);
        }
        [HttpGet("PendingRequest/{teamId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPendingMatchesRequestAsync(Guid teamId, int pageSize, int pageNumber)
        {
            var response = await _matchService.GetPendingMatchesRequests(teamId, new PaginationDto { pageNumber = pageNumber, pageSize = pageSize });
            return response.IsSuccess ? Success(response.Value) : Failed(response.Error!, 400);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMatchById(Guid id)
        {
            var response = await _matchService.GetMatchById(id);
            return response.IsSuccess ? Success(response.Value) : Failed(response.Error!, 400);
        }

        /// <summary>
        /// Create a new match
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateMatch([FromBody] CreateMatchDto match)
        {
            var response = await _matchService.CreateAsync(match);
            return response.IsSuccess ? Success(response.Value) : Failed(response.Error!, 400);
        }

        /// <summary>
        /// Update an existing match
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateMatchScoreAsync(Guid id, Guid captinId, int score)
        {
            var response = await _matchService.UpdateMatchScore(id, captinId,score);
            return response.IsSuccess ? Success(response.Value) : Failed(response.Error!, 400);

        }
        [HttpPut("Status/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangeStatusAsync(Guid id, MatchStatues statues)
        {
            var response = await _matchService.ChangeStatus(id, statues);
            return response.IsSuccess ? Success(response.Value) : Failed(response.Error!, 400);

        }

    }
}

