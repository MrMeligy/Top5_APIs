using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Top5.Api.Mapping;
using Top5.Business.Services;
using Top5.Contracts.DTOs;
using Top5.Domain.Entities;
using Top5.Domain.Models;

namespace Top5.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class PlayersController : BaseController
    {
        private readonly IPlayerService _playerService;
        private readonly IMapper _mapper;
        public PlayersController(IPlayerService playerService,IMapper mapper)
        {
            _playerService = playerService;
            _mapper = mapper;
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPlayerDto(Guid id)
        {
            var response = await _playerService.GetPlayerDtoById(id);
            return response.IsSuccess ? Success(response.Value) : Failed(response.Error!,400);

        }
        [HttpGet("Search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchPlayers(string userName)
        {
            var response = await _playerService.SearchPlayersAsync(userName);
            if (!response.IsSuccess)
            {
                return Failed(response.Error!,400);
            }
            var resDto = _mapper.Map<IEnumerable<PlayerDto>>(response.Value);
            return Success(resDto);

        }
        
        [HttpPut()]
        [ProducesResponseType(typeof(Player), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePlayer([FromBody] UpdatePlayerDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var id = Guid.Parse(userId!);
            var response = await _playerService.UpdateAsync(id, dto);
            if (!response.IsSuccess)
            {
                return Failed(response.Error!, 400);
            }
            var resDto = _mapper.Map<PlayerDto>(response.Value);
            return Success(resDto);
        }


        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePlayer(Guid id)
        {
            var response = await _playerService.DeleteAsync(id);
            
            return response.IsSuccess ? Success(response.Value) : Failed(response.Error!, 400);
        }
    }
}

