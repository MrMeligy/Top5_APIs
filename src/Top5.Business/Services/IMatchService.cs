using Top5.Domain.Entities;

namespace Top5.Business.Services
{
    public interface IMatchService
    {
        Task<Match?> GetByIdAsync(Guid id);
        Task<IEnumerable<Match>> GetAllAsync();
        Task<IEnumerable<Match>> GetAllWithTeamsAsync();
        Task<Match?> GetByIdWithTeamsAsync(Guid id);
        Task<Match> CreateAsync(Match match);
        Task<Match?> UpdateAsync(Guid id, Match match);
        Task<bool> DeleteAsync(Guid id);
    }
}

