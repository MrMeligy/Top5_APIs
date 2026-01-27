using Top5.Domain.Entities;

namespace Top5.Business.Services
{
    public interface ITeamPlayersService
    {
        Task<TeamPlayers?> GetByIdAsync(Guid id);
        Task<IEnumerable<TeamPlayers>> GetTeamPlayersAsync(Guid teamId);
        Task<IEnumerable<TeamPlayers>> GetAllAsync();
        Task<TeamPlayers> CreateAsync(TeamPlayers teamPlayers);
        Task<TeamPlayers?> UpdateAsync(Guid id, TeamPlayers teamPlayers);
        Task<bool> DeleteAsync(Guid id);
    }
}

