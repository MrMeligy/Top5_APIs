using AutoMapper;
using Top5.Business.Result;
using Top5.Contracts.DTOs;
using Top5.Data.Repositories;
using Top5.Domain.Entities;
using Top5.Domain.Models;

namespace Top5.Business.Services
{
    public class TeamPlayersService : ITeamPlayersService
    {
        private readonly ITeamPlayersRepository _repository;
        private readonly ITeamRepository _tmRepo;
        private readonly IMapper _mapper;

        public TeamPlayersService(ITeamPlayersRepository repository,IMapper mapper, ITeamRepository tmRepo)
        {
            _repository = repository;
            _mapper = mapper;
            _tmRepo = tmRepo;
        }

        public async Task<Result<TeamPlayers>> CreateAsync(CreateTeamPlayerDto dto)
        {
            try
            {
                var team = await _tmRepo.GetByIdAsync(dto.teamId);
                
                if (team == null)
                {
                    return Result<TeamPlayers>.Failure("Team Not Found");
                }
                if (await _repository.IsAtTeam(dto.teamId, dto.playerId))
                {
                    return Result<TeamPlayers>.Failure("Player Already in this Team");
                }
                var teamPlayer = _mapper.Map<TeamPlayers>(dto);
                var response = await _repository.AddAsync(teamPlayer);
                await _repository.SaveChangesAsync();
                return Result<TeamPlayers>.Success(response);
            }
            catch (Exception ex)
            {
                return Result<TeamPlayers>.Failure(ex.Message);
            }

        }

        public async Task<Result<TeamPlayers>> ExitFromTeam(Guid teamId,Guid playerId)
        {
            try
            {
                var team = await _tmRepo.GetByIdAsync(teamId);

                if (team == null)
                {
                    return Result<TeamPlayers>.Failure("Team Not Found");
                }
                var response = await _repository.ExitTeam(teamId, playerId);
                if (response == null)
                {
                    return Result<TeamPlayers>.Failure("Team or Player Not Found");
                }
                return Result<TeamPlayers>.Success(response);

            }
            catch (Exception ex)
            {
                return Result<TeamPlayers>.Failure(ex.Message);
            }
        }

        public async Task<Result<IEnumerable<TeamPlayerDto>>> GetPlayerTeams(Guid playerId)
        {
            try
            {
                var response = await _repository.GetPlayerTeams(playerId);
                var mapped = _mapper.Map<IEnumerable<TeamPlayerDto>>(response);
                return Result<IEnumerable<TeamPlayerDto>>.Success(mapped);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<TeamPlayerDto>>.Failure(ex.Message);
            }
        }

        public async Task<Result<IEnumerable<TeamPlayerDto>>> GetTeamPlayers(Guid teamId)
        {
            try
            {
                var team = await _tmRepo.GetByIdAsync(teamId);

                if (team == null)
                {
                    return Result<IEnumerable<TeamPlayerDto>>.Failure("Team Not Found");
                }
                var response = await _repository.GetTeamPlayers(teamId);
                var mapped = _mapper.Map<IEnumerable<TeamPlayerDto>>(response);
                return Result<IEnumerable<TeamPlayerDto>>.Success(mapped);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<TeamPlayerDto>>.Failure(ex.Message);
            }
        }
    }
}

