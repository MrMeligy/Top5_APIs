using System;
using System.Collections.Generic;
using System.Text;
using Top5.Contracts.DTOs;
using Top5.Contracts.Helper;
using Top5.Domain.Entities;

namespace Top5.Data.Repositories
{
    public interface IMatchPlayerRepository : IRepository<MatchPlayers>
    {
        Task<PaginationResponse<MatchPlayers>?> GetPlayerMatches(Guid playerId,int pageSize,int pageNumber);
        Task<IEnumerable<MatchPlayers>?> GetMatchPlayers(Guid matchId);
        Task<MatchPlayers?> GetPlayerStatsInMatchAsync(Guid matchId,Guid playerId);
        Task<PaginationResponse<MatchPlayers>?> GetPlayerMatchesByTeamAsync(Guid teamId, Guid playerId,int pageSize,int pageNumber);
        Task<PlayerStatsDto?> GetPlayerTeamStatsAsync(Guid teamId, Guid playerId);
        Task<PlayerStatsDto?> GetPlayerStats(Guid playerId);
        Task<TeamStatsDto?> GetTeamStatsByMatchAsync(Guid teamId, Guid matchId);

    }
}
