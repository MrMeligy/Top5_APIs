using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Top5.Contracts.DTOs;
using Top5.Contracts.Helper;
using Top5.Domain.Entities;
using Top5.Domain.Enums;

namespace Top5.Data.Repositories
{
    public interface IStatesRepository : IRepository<PlayerStats>
    {
        Task<PaginationResponse<PlayerStatesRankDto>> GetScorers(int pageSize, int pageNumber);
        Task<PaginationResponse<PlayerStatesRankDto>> GetAssists(int pageSize, int pageNumber);
        Task<PaginationResponse<PlayerStatesRankDto>> GetSaves(int pageSize, int pageNumber);
        Task<PaginationResponse<PlayerStatesRankDto>?> GetPlayerRankAsync(Guid playerId,RankType rankType);
    }
}
