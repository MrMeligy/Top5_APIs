using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Top5.Data.Projections;
using Top5.Domain.Models;

namespace Top5.Data.Repositories
{
    public class PlayerRepository:Repository<Player>,IPlayerRepository 
    {
        public PlayerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Player?> getByUserName(string userName)
        {
            var player = await _context.Players.AsNoTracking().FirstOrDefaultAsync(p=>p.username==userName);
            return player;
        }

        public async Task<PlayerDetailsProjection?> GetPlayerDtoAsync(Guid id)
        {
            var player = await _context.Players
                .AsNoTracking()
                .Where(p => p.id == id)
                .Select(p => new
                {
                    p.id,
                    p.username,
                    p.picUrl,
                    p.position,
                    p.dob,
                    p.phone,
                    p.level
                })
                .FirstOrDefaultAsync();

            if (player == null)
                return null;

            var stats = await _context.MatchPlayers
                .AsNoTracking()
                .Where(mp => mp.playerId == id)
                .GroupBy(_ => 1)
                .Select(g => new
                {
                    Goals = g.Sum(x => x.goals),
                    Assists = g.Sum(x => x.assists),
                    Saves = g.Sum(x => x.saves),
                    Matches = g.Count(),
                    rate = g.Average(x => x.rate)

                })
                .FirstOrDefaultAsync();

            var projection = new PlayerDetailsProjection
            {
                id = player.id,
                username = player.username,
                picUrl = player.picUrl,
                position = player.position,
                dob = player.dob,
                phone = player.phone,
                level = player.level,

                goals = stats?.Goals ?? 0,
                assists = stats?.Assists ?? 0,
                saves = stats?.Saves ?? 0,
                matchCount = stats?.Matches ?? 0,
                rate = stats?.rate ?? 0.0
            };

            return projection;
        }

        public async Task<bool> isExistAsync(Player player)
        {
            return await _context.Players
        .AsNoTracking()
        .AnyAsync(p =>
            p.username == player.username ||
            p.phone == player.phone
        );
        }
    }
}
