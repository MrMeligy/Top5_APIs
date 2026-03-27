using Top5.Business.Result;
using Top5.Contracts.DTOs;
using Top5.Domain.Entities;

namespace Top5.Business.Services
{
    public interface ITeamPlayersService
    {
        Task<Result<IEnumerable<TeamPlayerDto>>> GetTeamPlayers(Guid teamId);
        Task<Result<IEnumerable<TeamPlayerDto>>> GetPlayerTeams(Guid playerId);
        Task<Result<TeamPlayers>> CreateAsync(CreateTeamPlayerDto dto);
        Task<Result<TeamPlayers>> ExitFromTeam(Guid teamId,Guid playerId);
    }
}

