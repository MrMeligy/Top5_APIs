using Top5.Domain.Entities;

namespace Top5.Business.Services
{
    public interface ITeamService
    {
        Task<Team?> GetByIdAsync(Guid id);
        Task<IEnumerable<Team>> GetAllAsync();
        Task<Team> CreateAsync(Team team);
        Task<Team?> UpdateAsync(Guid id, Team team);
        Task<bool> DeleteAsync(Guid id);
    }
}

