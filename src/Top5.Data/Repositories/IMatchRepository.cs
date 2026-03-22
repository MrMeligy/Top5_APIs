using System;
using System.Collections.Generic;
using System.Text;
using Top5.Contracts.DTOs;
using Top5.Contracts.Helper;
using Top5.Domain.Entities;
using Top5.Domain.Enums;

namespace Top5.Data.Repositories
{
    public interface IMatchRepository : IRepository<Match> 
    {
        Task<Match?> GetMatchById(Guid id);
        Task<Match?> GetNextMatch(Guid teamId);
        Task<bool> ChangeStatus(Guid matchId, MatchStatues newStatus);
        Task<PaginationResponse<Match>> GetPendingMatchesSent(Guid teamId, int pageSize, int pageNumber);
        Task<PaginationResponse<Match>> GetPendingMatchesRequests(Guid teamId, int pageSize, int pageNumber);
        Task<IEnumerable<Match>> GetMatchesByDate(Guid teamId, DateOnly date);
        Task<bool> HasAnotherMatch(DateTime kickOff, DateTime endTime);
        Task<bool> RejectMatchesInSameTime(DateTime kickOff, DateTime endTime);
        Task<PaginationResponse<Match>> GetMatchesHistory(Guid teamId, int pageSize, int pageNumber);
        Task<PaginationResponse<Match>> GetTeamSchedule(Guid teamId, int pageSize, int pageNumber);
        Task<PaginationResponse<Match>> GetAllTeamMatches(Guid teamId, int pageSize,int pageNumber);
        Task<PaginationResponse<Match>> GetMatchesByStatus(Guid teamId,MatchStatues status, int pageSize, int pageNumber);
        Task<Match?> UpdateMatchScoreAsync(Guid id, Guid captinId, int score);
        Task<bool> IsScoreUpdated(Guid id);


    }
}
