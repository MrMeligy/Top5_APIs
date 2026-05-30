using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Top5.Business.Services;
using Top5.Contracts.DTOs;
using Top5.Contracts.Helper;
using Top5.Domain.Enums;

namespace Top5.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesController : BaseController
    {
        private readonly IStatesService _srv;

        public StatesController(IStatesService srv)
        {
            _srv = srv;
        }
        [HttpGet("Scores")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetScoresRanking(int pageSize, int pageNumber)
        {
            var response = await _srv.GetScorersRanking(pageSize, pageNumber);
            if (!response.IsSuccess)
            {
                return Failed(response.Error!, 400);
            }
            return Success(response.Value);

        }
        [HttpGet("Assists")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAssistsRanking(int pageSize, int pageNumber)
        {
            var response = await _srv.GetAssistsRanking(pageSize, pageNumber);
            if (!response.IsSuccess)
            {
                return Failed(response.Error!, 400);
            }
            return Success(response.Value);

        }
        [HttpGet("Saves")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSavesRanking(int pageSize, int pageNumber)
        {
            var response = await _srv.GetSavesRanking(pageSize, pageNumber);
            if (!response.IsSuccess)
            {
                return Failed(response.Error!, 400);
            }
            return Success(response.Value);

        }
        // has issue مبتجيبش الداتا صح 
        //[HttpGet("{id:guid}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<IActionResult> GetPlayerRanking(Guid id, RankType rankType)
        //{
        //    var response = await _srv.GetPlayerRankAsync(id, rankType);
        //    if (!response.IsSuccess)
        //    {
        //        return Failed(response.Error!, 400);
        //    }
        //    return Success(response.Value);

        //}


    }
}
