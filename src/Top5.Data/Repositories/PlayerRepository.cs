using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Top5.Contracts.Helper;
using Top5.Data.Projections;
using Top5.Domain.Entities;
using Top5.Domain.Models;

namespace Top5.Data.Repositories
{
    public class PlayerRepository:Repository<Player>,IPlayerRepository 
    {
        public PlayerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Guid?> isPhoneExist(string phone)
        {
            var player = await _context.Players.AsNoTracking().FirstOrDefaultAsync(p => p.phone == phone);
            return player?.id;
        }
        public async Task<Guid?> isUserNameExist(string username)
        {
            var player = await _context.Players.AsNoTracking().FirstOrDefaultAsync(p => p.username == username);
            return player?.id;
        }

        public async Task<Player?> getByUserName(string userName)
        {
            var player = await _context.Players.AsNoTracking().FirstOrDefaultAsync(p=>p.username==userName);
            return player;
        }

        public async Task<PlayerDetailsProjection> GetPlayerDtoAsync(Guid id)
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

            var team = await _context.TeamPlayers
                .AsNoTracking()
                .Where(t => t.playerId==id)
                .FirstOrDefaultAsync();
            var teamInfo = null as Team;
            if (team != null)
            {
                teamInfo = await _context.Teams
                    .AsNoTracking()
                    .Where(t => t.id == team.teamId)
                    .FirstOrDefaultAsync();
            }
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
                rate = stats?.rate ?? 0.0,

                team = teamInfo
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

        public async Task<PaginationResponse<Player>> SearchPlayersAsync(string userName, int pageSize, int pageNumber)
        {
            var query = _context.Players
            .Where(p => EF.Functions.Like(p.username, $"%{userName}%"));
            var totalCount = await query.CountAsync();
            var players = await query
                .OrderBy(p =>p.username)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).AsNoTracking().ToListAsync();
            return new PaginationResponse<Player>
            {
                Data = players,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
        }
    }
}
