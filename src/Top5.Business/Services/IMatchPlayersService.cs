using Top5.Business.Result;
using Top5.Contracts.DTOs;
using Top5.Contracts.Helper;
using Top5.Domain.Entities;

namespace Top5.Business.Services
{
    public interface IMatchPlayersService
    {
        Task<Result<PaginationResponse<MatchPlayerDto>?>> GetPlayerMatches(Guid playerId, int pageSize, int pageNumber);
        Task<Result<IEnumerable<MatchPlayerDto>?>> GetMatchPlayers(Guid matchId);
        Task<Result<MatchPlayerDto?>> GetPlayerStatsInMatchAsync(Guid matchId, Guid playerId);
        Task<Result<PaginationResponse<MatchPlayerDto>?>> GetPlayerMatchesByTeamAsync(Guid teamId, Guid playerId, int pageSize, int pageNumber);
        Task<Result<PlayerStatsDto?>> GetPlayerTeamStatsAsync(Guid teamId, Guid playerId);
        Task<Result<PlayerStatsDto?>> GetPlayerStats(Guid playerId);
        Task<Result<MatchPlayerDto>> CreateAsync(CreateMatchPlayerDto dto,Guid captinId);
    }
}

