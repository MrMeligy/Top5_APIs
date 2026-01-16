using Top5.Data.Repositories;
using Top5.Domain.Entities;

namespace Top5.Business.Services
{
    public class MatchPlayersService : IMatchPlayersService
    {
        private readonly IRepository<MatchPlayers> _repository;

        public MatchPlayersService(IRepository<MatchPlayers> repository)
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
    }
}

