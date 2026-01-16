using Top5.Data.Repositories;
using Top5.Domain.Models;

namespace Top5.Business.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IRepository<Player> _repository;

        public PlayerService(IRepository<Player> repository)
        {
            _repository = repository;
        }

        public async Task<Player?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Player>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Player> CreateAsync(Player player)
        {
            if (player.id == Guid.Empty)
            {
                player.id = Guid.NewGuid();
            }
            return await _repository.AddAsync(player);
        }

        public async Task<Player?> UpdateAsync(Guid id, Player player)
        {
            var existingPlayer = await _repository.GetByIdAsync(id);
            if (existingPlayer == null)
                return null;

            player.id = id;
            return await _repository.UpdateAsync(player);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}

