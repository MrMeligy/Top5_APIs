using Top5.Domain.Entities;

namespace Top5.Business.Services
{
    public interface IMatchService
    {
        Task<Match?> GetByIdAsync(Guid id);
        Task<IEnumerable<Match>> GetAllAsync();
        Task<Match> CreateAsync(Match match);
        Task<Match?> UpdateAsync(Guid id, Match match);
        Task<bool> DeleteAsync(Guid id);
    }
}

