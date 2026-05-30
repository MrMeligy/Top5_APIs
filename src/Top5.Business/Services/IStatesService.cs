using System;
using System.Collections.Generic;
using System.Text;
using Top5.Business.Result;
using Top5.Contracts.DTOs;
using Top5.Contracts.Helper;
using Top5.Domain.Entities;
using Top5.Domain.Enums;

namespace Top5.Business.Services
{
    public interface IStatesService
    {
        Task<Result<PaginationResponse<PlayerStatesRankDto>>> GetScorersRanking(int pageSize, int pageNumber);
        Task<Result<PaginationResponse<PlayerStatesRankDto>>> GetAssistsRanking(int pageSize, int pageNumber);
        Task<Result<PaginationResponse<PlayerStatesRankDto>>> GetSavesRanking(int pageSize, int pageNumber);
        Task<Result<PaginationResponse<PlayerStatesRankDto>?>> GetPlayerRankAsync(Guid playerId, RankType rankType);
    }
}
