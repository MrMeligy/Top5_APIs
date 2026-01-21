using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Top5.Domain.Entities;
namespace Top5.Data.Repositories
{
    public class MatchRepository : Repository<Match>, IMatchRepository
    {
        public MatchRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Match>> GetAllWithTeamsAsync()
        {
            return await _dbSet
                .Include(m => m.homeTeam)
                .Include(m => m.awayTeam)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Match?> GetByIdWithTeamsAsync(Guid id)
        {
            return await _dbSet
                .Include(m => m.homeTeam)
                .Include(m => m.awayTeam)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.id == id);
        }
    }
}
