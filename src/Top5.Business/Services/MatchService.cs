using AutoMapper;
using Top5.Business.Result;
using Top5.Contracts.DTOs;
using Top5.Contracts.Helper;
using Top5.Data.Repositories;
using Top5.Domain.Entities;
using Top5.Domain.Enums;

namespace Top5.Business.Services
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _repository;
        private readonly ITeamService _tmsrvc;
        private readonly IMapper _mapper;

        public MatchService(IMatchRepository repository, ITeamService tmsrvc, IMapper mapper)
        {
            _repository = repository;
            _tmsrvc = tmsrvc;
            _mapper = mapper;
        }

        public async Task<Result<bool>> ChangeStatus(Guid matchId, MatchStatues newStatus)
        {
            try
            {
               var response = await _repository.ChangeStatus(matchId, newStatus);
                return Result<bool>.Success(response);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
        }

        public async Task<Result<MatchDto>> CreateAsync(CreateMatchDto dto)
        {
            try
            {
                bool hasAnotherMatches = await _repository.HasAnotherMatch(dto.homeTeamId,dto.awayTeamId,dto.kickOff);
                if (hasAnotherMatches)
                {
                    return Result<MatchDto>.Failure("Team Has another match in this time");
                }
                var match = _mapper.Map<Match>(dto);
                var response = await _repository.AddAsync(match);
                var mapped = _mapper.Map<MatchDto>(response);
                return Result<MatchDto>.Success(mapped);
            }
            catch (Exception ex)
            {
                return Result<MatchDto>.Failure(ex.Message);
            }
        }

        public async Task<Result<PaginationResponse<MatchDto>>> GetAllTeamMatches(Guid teamId, PaginationDto pagination)
        {
            try
            {
                var response = await _repository.GetAllTeamMatches(teamId, pagination.pageSize,pagination.pageNumber);
                var mapped = _mapper.Map<PaginationResponse<MatchDto>>(response);
                return Result<PaginationResponse<MatchDto>>.Success(mapped);

            }
            catch (Exception ex)
            {
                return Result<PaginationResponse<MatchDto>>.Failure(ex.Message);
            }
        }

        public async Task<Result<MatchDto?>> GetMatchById(Guid id)
        {
            try
            {
                var response = await _repository.GetMatchById(id);
                var mapped = _mapper.Map<MatchDto>(response);
                return Result<MatchDto?>.Success(mapped);
            }
            catch (Exception ex)
            {
                return Result<MatchDto?>.Failure(ex.Message);
            }
        }

        public async Task<Result<IEnumerable<MatchDto>>> GetMatchesByDate(Guid teamId,DateOnly date)
        {
            var response = await _repository.GetMatchesByDate(teamId, date);
            var mapped = _mapper.Map<IEnumerable<MatchDto>>(response);
            return Result<IEnumerable<MatchDto>>.Success(mapped);
        }

        public async Task<Result<PaginationResponse<MatchDto>>> GetMatchesByStatus(Guid teamId,MatchStatues status, PaginationDto pagination)
        {
            try
            {
                var response = await _repository.GetMatchesByStatus(teamId, status, pagination.pageSize, pagination.pageNumber);
                var mapped = _mapper.Map<PaginationResponse<MatchDto>>(response);
                return Result<PaginationResponse<MatchDto>>.Success(mapped);
            }
            catch (Exception ex)
            {
                return Result<PaginationResponse<MatchDto>>.Failure(ex.Message);
            }
        }

        public async Task<Result<PaginationResponse<MatchDto>>> GetMatchesHistory(Guid teamId,PaginationDto dto)
        {
            try
            {
                var response = await _repository.GetMatchesHistory(teamId, dto.pageSize,dto.pageNumber);
                var mapped = _mapper.Map<PaginationResponse<MatchDto>>(response);
                return Result<PaginationResponse<MatchDto>>.Success(mapped);
            }
            catch (Exception ex)
            {
                return Result<PaginationResponse<MatchDto>>.Failure(ex.Message);
            }
        }

        public async Task<Result<MatchDto?>> GetNextMatch(Guid teamId)
        {
            try { 
                 var response = await _repository.GetNextMatch(teamId);
                var mapped = _mapper.Map<MatchDto>(response);
                return Result<MatchDto?>.Success(mapped);
            }
            catch (Exception ex)
            {
                return Result<MatchDto?>.Failure(ex.Message);
            }
        }

        public async Task<Result<PaginationResponse<MatchDto>>> GetPendingMatchesRequests(Guid teamId,PaginationDto dto)
        {
            try
            {
                var response = await _repository.GetPendingMatchesRequests(teamId,dto.pageSize,dto.pageNumber);
                var mapped = _mapper.Map<PaginationResponse<MatchDto>>(response);
                return Result<PaginationResponse<MatchDto>>.Success(mapped);
            }
            catch(Exception ex)
            {
                return Result<PaginationResponse<MatchDto>>.Failure(ex.Message);
            }
        }

        public async Task<Result<PaginationResponse<MatchDto>>> GetPendingMatchesSent(Guid teamId, PaginationDto dto)
        {
            try
            {
                var response = await _repository.GetPendingMatchesSent(teamId, dto.pageSize, dto.pageNumber);
                var mapped = _mapper.Map<PaginationResponse<MatchDto>>(response);
                return Result<PaginationResponse<MatchDto>>.Success(mapped);
            }
            catch (Exception ex)
            {
                return Result<PaginationResponse<MatchDto>>.Failure(ex.Message);
            }
        }
        public async Task<Result<PaginationResponse<MatchDto>>> GetTeamSchedule(Guid teamId,PaginationDto dto)
        {
            try
            {
                var response = await _repository.GetTeamSchedule(teamId, dto.pageSize, dto.pageNumber);
                var mapped = _mapper.Map<PaginationResponse<MatchDto>>(response);
                return Result<PaginationResponse<MatchDto>>.Success(mapped);
            }
            catch (Exception ex)
            {
                return Result<PaginationResponse<MatchDto>>.Failure(ex.Message);
            }
        }

        public async Task<Result<Match?>> UpdateMatchScore(Guid id, Guid captinId, int score)
        {
            try
            {
                var match = await _repository.GetByIdAsync(id);
                if (match == null)
                    return Result<Match?>.Failure("Match not found");
                var response = await _repository.UpdateMatchScoreAsync(id, captinId, score);
                bool isScoreUpdated = await _repository.IsScoreUpdated(id);
                if (isScoreUpdated)
                {
                    await _repository.ChangeStatus(id, MatchStatues.Completed);
                    if (match.isComptitve)
                    {
                        await _tmsrvc.UpdateStatsAsync(new UpdateTeamStatsDto
                        {
                            homeId = match.homeTeamId,
                            awayId = match.awayTeamId,
                            homeScore = match.homeScore,
                            awayScore = match.awayScore,
                        });
                    }
                }
                return Result<Match?>.Success(response);
            }
            catch (Exception ex)
            {
                return Result<Match?>.Failure(ex.Message);
            }
        }
    }
}

