using Top5.Domain.Entities;

namespace Top5.Business.Services
{
    public interface IMatchPlayersService
    {
        Task<MatchPlayers?> GetByIdAsync(Guid id);
        Task<IEnumerable<MatchPlayers>> GetAllAsync();
        Task<MatchPlayers> CreateAsync(MatchPlayers matchPlayers);
        Task<MatchPlayers?> UpdateAsync(Guid id, MatchPlayers matchPlayers);
        Task<bool> DeleteAsync(Guid id);
    }
}

