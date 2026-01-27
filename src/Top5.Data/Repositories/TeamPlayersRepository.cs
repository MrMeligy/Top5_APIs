using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Top5.Domain.Entities;

namespace Top5.Data.Repositories
{
    public class TeamPlayersRepository : Repository<TeamPlayers>, ITeamPlayersRepository
    {
        public TeamPlayersRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TeamPlayers>> GetTeamPlayersView(Guid teamId)
        {
            return await _dbSet.Where(tp => tp.teamId == teamId)
                               .Include(tp => tp.player)
                               .Include(tm=>tm.team)
                               .AsNoTracking()
                               .ToListAsync();
        }
    }
}
