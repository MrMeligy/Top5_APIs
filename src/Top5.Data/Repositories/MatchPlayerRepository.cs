using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Top5.Contracts.DTOs;
using Top5.Contracts.Helper;
using Top5.Domain.Entities;
using Top5.Domain.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Top5.Data.Repositories
{
    public class MatchPlayerRepository : Repository<MatchPlayers>, IMatchPlayerRepository
    {
        public MatchPlayerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<MatchPlayers>?> GetMatchPlayers(Guid matchId)
        {
            return await _dbSet.Where(mp => mp.matchId == matchId)
                .Include(mp => mp.player)
                .Include(mp => mp.team)
                .ToListAsync();
        }

        public async Task<PaginationResponse<MatchPlayers>?> GetPlayerMatches(Guid playerId, int pageSize, int pageNumber)
        {
            var query = _dbSet.Where(mp => mp.playerId == playerId);
            var data = await query
                .OrderBy(mp=>mp.CreatedOn)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(mp => mp.team)
                .Include(mp => mp.player)
                .ToListAsync();
            var totalCount = await query.CountAsync();
            return new PaginationResponse<MatchPlayers>
            {
                Data = data,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };

        }

        public async Task<PaginationResponse<MatchPlayers>?> GetPlayerMatchesByTeamAsync(Guid teamId, Guid playerId, int pageSize, int pageNumber)
        {
            var query = _dbSet.Where(mp => mp.playerId == playerId && mp.teamId==teamId);
            var data = await query
                .OrderBy(mp => mp.CreatedOn)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(mp => mp.team)
                .Include(mp => mp.player)
                .ToListAsync();
            var totalCount = await query.CountAsync();
            return new PaginationResponse<MatchPlayers>
            {
                Data = data,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
        }

        public async Task<PlayerStatsDto?> GetPlayerStats(Guid playerId)
        {
            return await _dbSet.Where(m => m.playerId == playerId)
                .GroupBy(m => new
                {
                    m.playerId,
                    m.player.username,
                    m.player.picUrl,
                    m.player.dob
                })
                .Select(g => new PlayerStatsDto{
                    playerId = g.Key.playerId,
                    name = g.Key.username,
                    picUrl = g.Key.picUrl,
                    dob = g.Key.dob,
                    goals = g.Sum(x => x.goals),
                    assists = g.Sum(x => x.assists),
                    saves = g.Sum(x => x.saves), 
                }).FirstOrDefaultAsync();

        }
        public async Task<MatchPlayers?> GetPlayerStatsInMatchAsync(Guid matchId, Guid playerId)
        {
            return await _dbSet.Where(mp=>mp.matchId == matchId && mp.playerId == playerId)
                .Include(mp=>mp.player)
                .Include(mp=>mp.team)
                .FirstOrDefaultAsync();
        }

        public async Task<PlayerStatsDto?> GetPlayerTeamStatsAsync(Guid teamId, Guid playerId)
        {
            return await _dbSet.Where(m => m.playerId == playerId && m.teamId == teamId)
                .GroupBy(m => new
                {
                    m.playerId,
                    m.player.username,
                    m.player.picUrl,
                    m.player.dob,
                    m.teamId,
                    teamName = m.team.name,
                    teamLogo = m.team.picUrl
                })
                .Select(g => new PlayerStatsDto
                {
                    playerId = g.Key.playerId,
                    name = g.Key.username,
                    picUrl = g.Key.picUrl,
                    dob = g.Key.dob,
                    teamId = g.Key.teamId,
                    teamName = g.Key.teamName,
                    logo = g.Key.teamLogo,
                    goals = g.Sum(x => x.goals),
                    assists = g.Sum(x => x.assists),
                    saves = g.Sum(x => x.saves),
                }).FirstOrDefaultAsync();
        }

        public async Task<TeamStatsDto?> GetTeamStatsByMatchAsync(Guid teamId, Guid matchId)
        {
            return await _dbSet.Where(m => m.teamId == teamId && m.matchId == matchId)
                .GroupBy(m => new
                {
                    m.teamId,
                    m.matchId,
                })
                .Select(g => new TeamStatsDto
                {
                    teamId = g.Key.teamId,
                    matchId = g.Key.matchId,
                    goals = g.Sum(x => x.goals),
                    assists = g.Sum(x => x.assists),
                }).FirstOrDefaultAsync();
        }
    }
}
