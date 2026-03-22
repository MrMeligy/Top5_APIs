using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Top5.Contracts.DTOs;
using Top5.Contracts.Helper;
using Top5.Domain.Entities;
using Top5.Domain.Enums;
namespace Top5.Data.Repositories
{
    public class MatchRepository : Repository<Match>, IMatchRepository
    {
        public MatchRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> ChangeStatus(Guid matchId, MatchStatues newStatus)
        {
           var rows = await _dbSet.Where(m => m.id == matchId)
                .ExecuteUpdateAsync(s => s.SetProperty(m => m.statues, newStatus));
            return rows > 0;
        }

        public async Task<PaginationResponse<Match>> GetAllTeamMatches(Guid teamId, int pageSize, int pageNumber)
        {
            var query = _dbSet
                .Where(m => m.homeTeamId == teamId || m.awayTeamId == teamId);

            var totalCount = await query.CountAsync();

            var matches = await query
                .AsNoTracking()
                .OrderBy(m => m.kickOff)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(m => m.homeTeam)
                .Include(m => m.awayTeam)
                .ToListAsync();

            return new PaginationResponse<Match>
            {
                Data = matches,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
        }

        public async Task<Match?> GetMatchById(Guid id)
        {
            return await _dbSet
                .Include(m => m.homeTeam)
                .Include(m => m.awayTeam)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.id == id);
        }

        public async Task<IEnumerable<Match>> GetMatchesByDate(Guid teamId,DateOnly date)
        {
            return await _dbSet
                .Include(m => m.homeTeam)
                .Include(m => m.awayTeam) 
                .Where(m => (m.homeTeamId == teamId || m.awayTeamId == teamId) && DateOnly.FromDateTime(m.kickOff) == date && m.statues != MatchStatues.Rejected) //DateOnly.FromDateTime(dateTime)
                .OrderBy(m => m.kickOff)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<PaginationResponse<Match>> GetMatchesByStatus(Guid teamId,MatchStatues status, int pageSize, int pageNumber)
        {
            var query = _dbSet
                .Where(m => (m.homeTeamId == teamId || m.awayTeamId == teamId) && m.statues == status);

            var totalCount = await query.CountAsync();

            var matches = await query
                .AsNoTracking()
                .OrderBy(m => m.kickOff)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(m => m.homeTeam)
                .Include(m => m.awayTeam)
                .ToListAsync();

            return new PaginationResponse<Match>
            {
                Data = matches,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
        }

        public async Task<PaginationResponse<Match>> GetMatchesHistory(Guid teamId, int pageSize, int pageNumber)
        {
            var query = _dbSet
                .Where(m => (m.homeTeamId == teamId || m.awayTeamId == teamId) && m.statues == MatchStatues.Completed);

            var totalCount = await query.CountAsync();

            var matches = await query
                .AsNoTracking()
                .OrderBy(m => m.kickOff)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(m => m.homeTeam)
                .Include(m => m.awayTeam)
                .ToListAsync();

            return new PaginationResponse<Match>
            {
                Data = matches,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
        }

        public async Task<Match?> GetNextMatch(Guid teamId)
        {
            return await _dbSet
                .Where(m => (m.homeTeamId == teamId || m.awayTeamId == teamId) && m.kickOff > DateTime.UtcNow && m.statues == MatchStatues.Accepted)
                .Include(m => m.homeTeam)
                .Include(m => m.awayTeam)
                .OrderBy(m => m.kickOff)
                .FirstOrDefaultAsync();
        }

        public async Task<PaginationResponse<Match>> GetPendingMatchesRequests(Guid teamId, int pageSize, int pageNumber)
        {
            var query = _dbSet
                .Where(m => m.awayTeamId == teamId && m.statues == MatchStatues.Pending);

            var totalCount = await query.CountAsync();

            var matches = await query
                .OrderBy(m => m.kickOff)
                .AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(m => m.homeTeam)
                .Include(m => m.awayTeam)
                .ToListAsync();

            return new PaginationResponse<Match>
            {
                Data = matches,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
        }

        public async Task<PaginationResponse<Match>> GetPendingMatchesSent(Guid teamId, int pageSize, int pageNumber)
        {
            var query = _dbSet
                .Where(m => m.homeTeamId == teamId && m.statues == MatchStatues.Pending);

            var totalCount = await query.CountAsync();

            var matches = await query
                .AsNoTracking()
                .OrderBy(m => m.kickOff)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(m => m.homeTeam)
                .Include(m => m.awayTeam)
                .ToListAsync();

            return new PaginationResponse<Match>
            {
                Data = matches,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
        }

        public async Task<PaginationResponse<Match>> GetTeamSchedule(Guid teamId, int pageSize, int pageNumber)
        {
            var query = _dbSet
                        .Where(m => (m.homeTeamId == teamId || m.awayTeamId == teamId)
                && m.statues == MatchStatues.Accepted
                && m.kickOff >= DateTime.UtcNow);

                var totalCount = await query.CountAsync();

                var matches = await query
                    .AsNoTracking()
                    .OrderBy(m => m.kickOff)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Include(m => m.homeTeam)
                    .Include(m => m.awayTeam)
                    .ToListAsync();

                return new PaginationResponse<Match>
                {
                    Data = matches,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalCount = totalCount,
                    TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
                };
        }

        public async Task<bool> HasAnotherMatch(DateTime kickOff,DateTime endTime)
        {
            return await _dbSet.AnyAsync(m =>
                m.kickOff < endTime &&
                m.endTime > kickOff &&
                m.statues == MatchStatues.Accepted
            );
        }

        public async Task<bool> IsScoreUpdated(Guid id)
        {
            return await _dbSet
                .AsNoTracking()
                .AnyAsync(m => m.id == id
                    && m.IsHomeCaptinUpdated
                    && m.IsAwayCaptinUpdated);
        }

        public async Task<bool> RejectMatchesInSameTime(DateTime kickOff, DateTime endTime)
        {
            await _dbSet.Where(m =>
                m.kickOff < endTime &&
                m.endTime > kickOff &&
                m.statues == MatchStatues.Accepted
                ).ExecuteUpdateAsync(s => s
                .SetProperty(m => m.statues, MatchStatues.Rejected)
            );
            return true;
        }

        public async Task<Match?> UpdateMatchScoreAsync(Guid id, Guid captinId, int score)
        {
            var isHomeTeam = _dbSet.Any(m => m.id == id && m.homeTeamId == captinId);
            var isAwayTeam = false;
            if (!isHomeTeam)
                isAwayTeam = _dbSet.Any(m => m.id == id && m.awayTeamId == captinId);
            if (isHomeTeam)
            {
                await _dbSet.Where(m => m.id == id)
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(m => m.homeScore, score)
                        .SetProperty(m => m.IsHomeCaptinUpdated, true));
            }
            else if (isAwayTeam)
            {
                await _dbSet.Where(m => m.id == id)
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(m => m.awayScore, score)
                        .SetProperty(m => m.IsAwayCaptinUpdated, true));
            }
            else
            {
                return null;
            }
            return await GetMatchById(id);
        }
    }
}
