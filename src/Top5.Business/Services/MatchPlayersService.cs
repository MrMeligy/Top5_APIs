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

        public Task<MatchPlayers?> GetByMatchPlayerViewAsync(Guid id)
        {
            return _repository.GetByMatchPlayerViewAsync(id);
            
        }

        public Task<IEnumerable<MatchPlayers>?> GetByPlayerMatchesAsync(Guid playerId)
        {
            return _repository.GetByPlayerMatchesAsync(playerId);
        }

        public Task<IEnumerable<MatchPlayers>?> GetByMatchPlayersViewAsync(Guid matchId)
        {
            return _repository.GetByMatchPlayersViewAsync(matchId);
        }

        public Task<MatchPlayers?> GetMatchAndPlayerAsync(Guid matchId, Guid playerId)
        {
            return _repository.GetMatchAndPlayerAsync(matchId, playerId);
        }
    }
}

