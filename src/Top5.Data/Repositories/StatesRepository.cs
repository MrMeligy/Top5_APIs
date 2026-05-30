using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Top5.Contracts.DTOs;
using Top5.Contracts.Helper;
using Top5.Domain.Entities;
using Top5.Domain.Enums;
using Top5.Domain.Models;

namespace Top5.Data.Repositories
{
    public class StatesRepository : Repository<PlayerStats>, IStatesRepository
    {
        public StatesRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<PaginationResponse<PlayerStatesRankDto>> GetAssists(int pageSize, int pageNumber)
        {
            var query = _context.PlayerStats.Include(ps => ps.player)
                .OrderByDescending(ps => ps.assists)
                .Select(ps => new PlayerStatesRankDto
                {
                    PlayerId = ps.PlayerId,
                    PlayerName = ps.player.username,
                    PlayerPicture = ps.player.picUrl,
                    PlayerPosition = ps.player.position,
                    goals = ps.goals,
                    assists = ps.assists,
                    saves = ps.saves,
                    matchCount = ps.matchCount,
                    ModifiedOn = ps.ModifiedOn
                });
                
            var totalCount = await query.CountAsync();
            var players = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).AsNoTracking().ToListAsync();
            return new PaginationResponse<PlayerStatesRankDto>
            {
                Data = players,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
        }

        public async Task<PaginationResponse<PlayerStatesRankDto>> GetSaves(int pageSize, int pageNumber)
        {
            var query = _context.PlayerStats.Include(ps => ps.player)
                .OrderByDescending(ps => ps.saves)
                .Select(ps => new PlayerStatesRankDto
                {
                    PlayerId = ps.PlayerId,
                    PlayerName = ps.player.username,
                    PlayerPicture = ps.player.picUrl,
                    PlayerPosition = ps.player.position,
                    goals = ps.goals,
                    assists = ps.assists,
                    saves = ps.saves,
                    matchCount = ps.matchCount,
                    ModifiedOn = ps.ModifiedOn
                });
            var totalCount = await query.CountAsync();
            var players = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).AsNoTracking().ToListAsync();
            return new PaginationResponse<PlayerStatesRankDto>
            {
                Data = players,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
        }

        public async Task<PaginationResponse<PlayerStatesRankDto>> GetScorers(int pageSize, int pageNumber)
        {
            var query = _context.PlayerStats.Include(ps => ps.player)
                .OrderByDescending(ps => ps.goals)
                .Select(ps => new PlayerStatesRankDto
                {
                    PlayerId = ps.PlayerId,
                    PlayerName = ps.player.username,
                    PlayerPicture = ps.player.picUrl,
                    PlayerPosition = ps.player.position,
                    goals = ps.goals,
                    assists = ps.assists,
                    saves = ps.saves,
                    matchCount = ps.matchCount,
                    ModifiedOn = ps.ModifiedOn
                });
            var totalCount = await query.CountAsync();
            var players = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).AsNoTracking().ToListAsync();
            return new PaginationResponse<PlayerStatesRankDto>
            {
                Data = players,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
        }

        public async Task<PaginationResponse<PlayerStatesRankDto>?> GetPlayerRankAsync(Guid playerId,RankType rankType)
        {
            var playerStats = await _context.PlayerStats
                .FirstOrDefaultAsync(x => x.PlayerId == playerId);

            if (playerStats == null)
                return null;

            int playerValue = rankType switch
            {
                RankType.Goals => playerStats.goals,
                RankType.Assists => playerStats.assists,
                RankType.Saves => playerStats.saves,
                _ => 0
            };

            var query = rankType switch
            {
                RankType.Goals => _context.PlayerStats.OrderByDescending(x => x.goals),
                RankType.Assists => _context.PlayerStats.OrderByDescending(x => x.assists),
                RankType.Saves => _context.PlayerStats.OrderByDescending(x => x.saves),
                _ => _context.PlayerStats.OrderByDescending(x => x.goals)
            };

            var playerRank = rankType switch
            {
                RankType.Goals =>
                    await _context.PlayerStats.CountAsync(x => x.goals > playerValue),

                RankType.Assists =>
                    await _context.PlayerStats.CountAsync(x => x.assists > playerValue),

                RankType.Saves =>
                    await _context.PlayerStats.CountAsync(x => x.saves > playerValue),

                _ => 0
            };

            var totalCount = await _context.PlayerStats.CountAsync();

            int skip = totalCount <= 3
                ? 0
                : playerRank == 0
                    ? 0
                    : playerRank >= totalCount - 1
                        ? totalCount - 3
                        : playerRank - 1;

            var result = await query
                .Include(x => x.player)
                .Select(ps => new PlayerStatesRankDto
                {
                    PlayerId = ps.PlayerId,
                    PlayerName = ps.player.username,
                    PlayerPicture = ps.player.picUrl,
                    PlayerPosition = ps.player.position,
                    goals = ps.goals,
                    assists = ps.assists,
                    saves = ps.saves,
                    matchCount = ps.matchCount,
                    ModifiedOn = ps.ModifiedOn
                })
                .Skip(skip)
                .Take(Math.Min(3, totalCount))
                .ToListAsync();

            return new PaginationResponse<PlayerStatesRankDto>
            {
                Data = result,
                PageNumber = 1,
                PageSize = result.Count,
                TotalCount = result.Count,
                TotalPages = 1
            };
        }
    }
}
