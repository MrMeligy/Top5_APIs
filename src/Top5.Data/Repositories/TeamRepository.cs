using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Top5.Domain.Entities;

namespace Top5.Data.Repositories
{
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        public TeamRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Team?> GetByIdViewAsync(Guid id)
        {
            return await _dbSet.Where(t => t.id == id)
                .Include(c => c.captin)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Team?>> GetTeamsViewAsync()
        {
            return await _dbSet
                .Include(c => c.captin)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
