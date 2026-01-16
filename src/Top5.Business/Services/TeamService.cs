using Top5.Data.Repositories;
using Top5.Domain.Entities;

namespace Top5.Business.Services
{
    public class TeamService : ITeamService
    {
        private readonly IRepository<Team> _repository;

        public TeamService(IRepository<Team> repository)
        {
            _repository = repository;
        }

        public async Task<Team?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Team>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Team> CreateAsync(Team team)
        {
            if (team.id == Guid.Empty)
            {
                team.id = Guid.NewGuid();
            }
            return await _repository.AddAsync(team);
        }

        public async Task<Team?> UpdateAsync(Guid id, Team team)
        {
            var existingTeam = await _repository.GetByIdAsync(id);
            if (existingTeam == null)
                return null;

            team.id = id;
            return await _repository.UpdateAsync(team);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}

