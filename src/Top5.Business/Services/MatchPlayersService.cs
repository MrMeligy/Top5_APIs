using AutoMapper;
using System.Text.RegularExpressions;
using Top5.Business.Result;
using Top5.Contracts.DTOs;
using Top5.Contracts.Helper;
using Top5.Data.Repositories;
using Top5.Domain.Entities;

namespace Top5.Business.Services
{
    public class MatchPlayersService : IMatchPlayersService
    {
        private readonly IMatchPlayerRepository _repository;
        private readonly IMatchRepository _matchRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly ITeamPlayersRepository _teamPlayerRepository;
        private readonly IMapper _mapper;

        public MatchPlayersService(IMatchPlayerRepository repository,IMatchRepository matchRepository,
            ITeamRepository teamRepository,IMapper mapper,ITeamPlayersRepository teamPlayersRepository)
        {
            _repository = repository;
            _matchRepository = matchRepository;
            _teamRepository = teamRepository;
            _teamPlayerRepository = teamPlayersRepository;
            _mapper = mapper;
        }

        public async Task<Result<MatchPlayerDto>> CreateAsync(CreateMatchPlayerDto dto,Guid captinId)
        {
            try
            {
                var match = await _matchRepository.GetByIdAsync(dto.matchId);
                if (match == null)
                {
                    return Result<MatchPlayerDto>.Failure("Match Not Found");
                }
                var team = await _teamRepository.GetByIdAsync(dto.teamId);
                if (team == null)
                {
                    return Result<MatchPlayerDto>.Failure("Team Not Found");
                }
                // add check that player in that team
                if(await _teamPlayerRepository.IsAtTeam(dto.teamId, dto.playerId)==false)
                {
                    return Result<MatchPlayerDto>.Failure("The Player Is Not In this Team!");
                }
                if (match.homeTeamId != team.id && match.awayTeamId != team.id) 
                { 
                    return Result<MatchPlayerDto>.Failure("Team Not In This Match");    
                }
                if (team.captinId != captinId)
                {
                    return Result<MatchPlayerDto>.Failure("Just Team Captin Can Add Stats");
                }
                if (dto.goals < 0 || dto.assists < 0 || dto.saves < 0)
                {
                    return Result<MatchPlayerDto>.Failure("Invalid States Value");
                }
                var teamState = await _repository.GetTeamStatsByMatchAsync(dto.teamId, dto.matchId);
                int teamScore = 0;
                if (dto.teamId == match.homeTeamId)
                {
                    teamScore = match.homeScore;
                } else {
                    teamScore = match.awayScore;
                }
                if(await _repository.GetPlayerStatsInMatchAsync(dto.matchId, dto.playerId) != null)
                {
                    return Result<MatchPlayerDto>.Failure("This Player States Already Added In this Match!");
                }
                if (dto.goals + dto.assists + (teamState?.goals ?? 0) > teamScore) 
                {
                   return Result<MatchPlayerDto>.Failure("The Goals More Than Match Score!");
                }
                if (dto.assists + (teamState?.assists ?? 0) > teamScore) 
                {
                   return Result<MatchPlayerDto>.Failure("The Assists More Than Match Score!");
                }
                var matchPlayer = _mapper.Map<MatchPlayers>(dto);
                var response = await _repository.AddAsync(matchPlayer);
                var mapped = _mapper.Map<MatchPlayerDto>(response);
                return Result<MatchPlayerDto>.Success(mapped);

            }
            catch (Exception ex)
            {
                return Result<MatchPlayerDto>.Failure(ex.Message);
            }
        }

        public async Task<Result<IEnumerable<MatchPlayerDto>?>> GetMatchPlayers(Guid matchId)
        {
            try
            {
                var response = await _repository.GetMatchPlayers(matchId);
                var mapped = _mapper.Map<IEnumerable<MatchPlayerDto>?>(response);
                return Result<IEnumerable<MatchPlayerDto>?>.Success(mapped);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<MatchPlayerDto>?>.Failure(ex.Message);
            }
        }

        public async Task<Result<PaginationResponse<MatchPlayerDto>?>> GetPlayerMatches(Guid playerId, int pageSize, int pageNumber)
        {
            try
            {
                var response = await _repository.GetPlayerMatches(playerId,pageSize,pageNumber);
                var mapped = _mapper.Map<PaginationResponse<MatchPlayerDto>?>(response);
                return Result<PaginationResponse<MatchPlayerDto>?>.Success(mapped);
            }
            catch (Exception ex)
            {
                return Result<PaginationResponse<MatchPlayerDto>?>.Failure(ex.Message);
            }

        }

        public async Task<Result<PaginationResponse<MatchPlayerDto>?>> GetPlayerMatchesByTeamAsync(Guid teamId, Guid playerId, int pageSize, int pageNumber)
        {
            try
            {
                var response = await _repository.GetPlayerMatchesByTeamAsync(teamId,playerId,pageSize, pageNumber);
                var mapped = _mapper.Map<PaginationResponse<MatchPlayerDto>?>(response);
                return Result<PaginationResponse<MatchPlayerDto>?>.Success(mapped);
            }
            catch (Exception ex)
            {
                return Result<PaginationResponse<MatchPlayerDto>?>.Failure(ex.Message);
            }
        }

        public async Task<Result<PlayerStatsDto?>> GetPlayerStats(Guid playerId)
        {
            try
            {
                var response =  await _repository.GetPlayerStats(playerId);
                return Result<PlayerStatsDto?>.Success(response);
            }
            catch (Exception ex)
            {
                return Result<PlayerStatsDto?>.Failure(ex.Message);
            }
        }

        public async Task<Result<MatchPlayerDto?>> GetPlayerStatsInMatchAsync(Guid matchId, Guid playerId)
        {
            try
            {
                var response = await _repository.GetPlayerStatsInMatchAsync(matchId, playerId);
                var mapped = _mapper.Map<MatchPlayerDto>(response);
                return Result<MatchPlayerDto?>.Success(mapped);
            }
            catch (Exception ex)
            {
                return Result<MatchPlayerDto?>.Failure(ex.Message);
            }
        }

        public async Task<Result<PlayerStatsDto?>> GetPlayerTeamStatsAsync(Guid teamId, Guid playerId)
        {
            try
            {
                var response = await _repository.GetPlayerTeamStatsAsync(teamId,playerId);
                return Result<PlayerStatsDto?>.Success(response);
            }
            catch (Exception ex)
            {
                return Result<PlayerStatsDto?>.Failure(ex.Message);
            }
        }
    }
}

