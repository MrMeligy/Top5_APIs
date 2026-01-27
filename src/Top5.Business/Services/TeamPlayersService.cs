using Top5.Data.Repositories;
using Top5.Domain.Entities;

namespace Top5.Business.Services
{
    public class TeamPlayersService : ITeamPlayersService
    {
        private readonly ITeamPlayersRepository _repository;

        public TeamPlayersService(ITeamPlayersRepository repository)
        {
            _repository = repository;
        }

        public async Task<TeamPlayers?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<TeamPlayers>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<TeamPlayers> CreateAsync(TeamPlayers teamPlayers)
        {
            if (teamPlayers.id == Guid.Empty)
            {
                teamPlayers.id = Guid.NewGuid();
            }
            return await _repository.AddAsync(teamPlayers);
        }

        public async Task<TeamPlayers?> UpdateAsync(Guid id, TeamPlayers teamPlayers)
        {
            var existingTeamPlayers = await _repository.GetByIdAsync(id);
            if (existingTeamPlayers == null)
                return null;

            teamPlayers.id = id;
            return await _repository.UpdateAsync(teamPlayers);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<TeamPlayers>> GetTeamPlayersAsync(Guid teamId)
        {
            return await _repository.GetTeamPlayersView(teamId);
        }
    }
}

