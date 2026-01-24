using Top5.Data.Repositories;
using Top5.Domain.Entities;

namespace Top5.Business.Services
{
    public class MatchPlayersService : IMatchPlayersService
    {
        private readonly IMatchPlayerRepository _repository;

        public MatchPlayersService(IMatchPlayerRepository repository)
        {
            _repository = repository;
        }

        public async Task<MatchPlayers?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<MatchPlayers>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<MatchPlayers> CreateAsync(MatchPlayers matchPlayers)
        {
            if (matchPlayers.id == Guid.Empty)
            {
                matchPlayers.id = Guid.NewGuid();
            }
            return await _repository.AddAsync(matchPlayers);
        }

        public async Task<MatchPlayers?> UpdateAsync(Guid id, MatchPlayers matchPlayers)
        {
            var existingMatchPlayers = await _repository.GetByIdAsync(id);
            if (existingMatchPlayers == null)
                return null;

            matchPlayers.id = id;
            return await _repository.UpdateAsync(matchPlayers);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<MatchPlayers?> GetByMatchPlayerViewAsync(Guid id)
        {
            return await _repository.GetByMatchPlayerViewAsync(id);
            
        }

        public async Task<IEnumerable<MatchPlayers>?> GetByPlayerMatchesAsync(Guid playerId)
        {
            return await _repository.GetByPlayerMatchesAsync(playerId);
        }

        public async Task<IEnumerable<MatchPlayers>?> GetByMatchPlayersViewAsync(Guid matchId)
        {
            return await _repository.GetByMatchPlayersViewAsync(matchId);
        }

        public async Task<MatchPlayers?> GetMatchAndPlayerAsync(Guid matchId, Guid playerId)
        {
            return await _repository.GetMatchAndPlayerAsync(matchId, playerId);
        }

        public async Task<IEnumerable<MatchPlayers>?> GetPlayerTeamStatsAsync(Guid teamId, Guid playerId)
        {
            return await _repository.GetPlayerTeamStatsAsync(teamId, playerId);
        }
    }
}

