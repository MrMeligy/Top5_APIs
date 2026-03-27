using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Top5.Domain.Entities;
using Top5.Domain.Models;

namespace Top5.Data.Repositories
{
    public class TeamPlayersRepository : Repository<TeamPlayers>, ITeamPlayersRepository
    {
        public TeamPlayersRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<TeamPlayers?> ExitTeam(Guid teamId, Guid playerId)
        {
            var row = await _dbSet.FirstOrDefaultAsync(tp => tp.teamId == teamId && tp.playerId == playerId);
            if (row == null)
            {
                return null;
            }
            if (row.IsLeft == true)
            {
                return row;
            }
            row.IsLeft = true;
            row.LeftTime = DateTime.Now;
            await _context.SaveChangesAsync();
            return row;
        }

        public async Task<IEnumerable<TeamPlayers>> GetPlayerTeams(Guid playerId)
        {
            return await _dbSet.Where(tp => tp.playerId == playerId && !tp.IsLeft)
                .OrderBy(tp => tp.JoinedOn)
                .Include(tp => tp.player)
                .Include(tp => tp.team)
                .ToListAsync();
                
        }

        public async Task<IEnumerable<TeamPlayers>> GetTeamPlayers(Guid teamId)
        {
            return await _dbSet.Where(tp => tp.teamId== teamId && !tp.IsLeft)
                .OrderBy(tp => tp.JoinedOn)
                .Include(tp => tp.player)
                .Include(tp => tp.team)
                .ToListAsync();
        }

        public async Task<bool> IsAtTeam(Guid teamId, Guid playerId)
        {
            return await _dbSet.AnyAsync(tp => tp.teamId == teamId && tp.playerId == playerId && !tp.IsLeft);
        }
    }
}
