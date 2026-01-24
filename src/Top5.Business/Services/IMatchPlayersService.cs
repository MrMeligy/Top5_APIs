using Top5.Domain.Entities;

namespace Top5.Business.Services
{
    public interface IMatchPlayersService
    {
        public Task<MatchPlayers?> GetByMatchPlayerViewAsync(Guid id);
        public Task<IEnumerable<MatchPlayers>?> GetByPlayerMatchesAsync(Guid playerId);
        public Task<IEnumerable<MatchPlayers>?> GetByMatchPlayersViewAsync(Guid matchId);
        public Task<MatchPlayers?> GetMatchAndPlayerAsync(Guid matchId, Guid playerId);
        Task<MatchPlayers?> GetByIdAsync(Guid id);
        Task<IEnumerable<MatchPlayers>> GetAllAsync();
        Task<MatchPlayers> CreateAsync(MatchPlayers matchPlayers);
        Task<MatchPlayers?> UpdateAsync(Guid id, MatchPlayers matchPlayers);
        Task<bool> DeleteAsync(Guid id);
    }
}

