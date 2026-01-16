using Top5.Data.Repositories;
using Top5.Domain.Entities;

namespace Top5.Business.Services
{
    public class MatchService : IMatchService
    {
        private readonly IRepository<Match> _repository;

        public MatchService(IRepository<Match> repository)
        {
            _repository = repository;
        }

        public async Task<Match?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Match>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Match> CreateAsync(Match match)
        {
            if (match.id == Guid.Empty)
            {
                match.id = Guid.NewGuid();
            }
            return await _repository.AddAsync(match);
        }

        public async Task<Match?> UpdateAsync(Guid id, Match match)
        {
            var existingMatch = await _repository.GetByIdAsync(id);
            if (existingMatch == null)
                return null;

            match.id = id;
            return await _repository.UpdateAsync(match);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}

