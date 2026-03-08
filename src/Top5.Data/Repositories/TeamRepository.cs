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

        public async Task<IEnumerable<Team>> LeaderBoard(int pageNumber,int pageSize)
        {
            return await _dbSet
                .Where(t => t.matchCount > 0)
                .Include(c => c.captin)
                .OrderByDescending(t => t.points)
                .ThenByDescending(t => t.goals - t.goalsAgainest)
                .ThenByDescending(t => t.goals)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Team>> SearchTeam(int pageNumber, int pageSize,string name)
        {
            return await _dbSet
                .Where(t => EF.Functions.Like(t.name, $"%{name}%"))
                .Include(c => c.captin)
                .AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
