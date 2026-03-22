using Top5.Business.Result;
using Top5.Contracts.DTOs;
using Top5.Contracts.Helper;
using Top5.Domain.Entities;
using Top5.Domain.Enums;

namespace Top5.Business.Services
{
    public interface IMatchService
    {
        Task<Result<MatchDto?>> GetMatchById(Guid id);
        Task<Result<MatchDto?>> GetNextMatch(Guid teamId);
        Task<Result<bool>> ChangeStatus(Guid matchId, MatchStatues newStatus,Guid captinId);
        Task<Result<PaginationResponse<MatchDto>>> GetPendingMatchesSent(Guid teamId,PaginationDto dto);
        Task<Result<PaginationResponse<MatchDto>>> GetPendingMatchesRequests(Guid teamId, PaginationDto dto);
        Task<Result<IEnumerable<MatchDto>>> GetMatchesByDate(Guid teamId,DateOnly date);
        Task<Result<PaginationResponse<MatchDto>>> GetMatchesHistory(Guid teamId,PaginationDto dto);
        Task<Result<PaginationResponse<MatchDto>>> GetTeamSchedule(Guid teamId,PaginationDto dto);
        Task<Result<PaginationResponse<MatchDto>>> GetAllTeamMatches(Guid teamId,PaginationDto pagination);
        Task<Result<PaginationResponse<MatchDto>>> GetMatchesByStatus(Guid teamId,MatchStatues status, PaginationDto pagination);
        Task<Result<MatchDto>> CreateAsync(CreateMatchDto match,Guid captinId);
        Task<Result<Match?>> UpdateMatchScore(Guid id, Guid captinId ,int score);
        //Result<Task<bool>> DeleteAsync(Guid id);
    }
}

