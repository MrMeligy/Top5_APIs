using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Top5.Domain.Entities;

namespace Top5.Data.Repositories
{
    public class MatchPlayerRepository : Repository<MatchPlayers>, IMatchPlayerRepository
    {
        public MatchPlayerRepository(ApplicationDbContext context) : base(context)
        {
        }


        public async Task<IEnumerable<MatchPlayers>?> GetByMatchPlayersViewAsync(Guid matchId)
        {
            return await _dbSet.Where(mp => mp.matchId == matchId)
                .Include(mp => mp.player)
                .Include(mp => mp.team)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<MatchPlayers?> GetByMatchPlayerViewAsync(Guid id)
        {

            return await _dbSet
                .Include(mp => mp.player)
                .Include(mp => mp.team)
                .AsNoTracking()
                .FirstOrDefaultAsync(mp => mp.id == id);
        }

        public async Task<IEnumerable<MatchPlayers>?> GetByPlayerMatchesAsync(Guid playerId)
        {
            return await _dbSet.Where(mp => mp.playerId == playerId)
                .Include(mp => mp.player)
                .Include(mp => mp.team)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<MatchPlayers?> GetMatchAndPlayerAsync(Guid matchId, Guid playerId)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(mp => mp.matchId == matchId && mp.playerId == playerId);
        }
    }
}
