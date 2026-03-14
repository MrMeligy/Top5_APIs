using Top5.Business.Result;
using Top5.Contracts.DTOs;
using Top5.Data.Repositories;
using Top5.Domain.Entities;

namespace Top5.Business.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _repository;

        public TeamService(ITeamRepository repository)
        {
            _repository = repository;
        }
        public async Task<Result<Team?>> GetByIdViewAsync(Guid id)
        {
            try
            {
                var team = await _repository.GetByIdViewAsync(id);
                if (team == null)
                    Result<Team?>.Failure("Team not found");
                return Result<Team?>.Success(team);
            }
            catch (Exception ex)
            {
                return Result<Team?>.Failure($"An error occurred while retrieving the team: {ex.Message}");
            }
        }

        public async Task<Result<Team>> CreateAsync(CreateTeamDto team,Guid capId)
        {
            try
            {
                var newTeam = new Team
                {
                    id = Guid.NewGuid(),
                    name = team.name,
                    captinId = capId,
                    picUrl = team.picUrl
                };
                return Result<Team>.Success(await _repository.AddAsync(newTeam));
            }
            catch (Exception ex)
            {
                return Result<Team>.Failure($"An error occurred while creating the team: {ex.Message}");
            }
        }

        public async Task<Result<Team?>> UpdateInfoAsync(Guid id,Guid capId, UpdateTeamInfoDto team)
        {
            try
            {
                var existingTeam = await _repository.GetByIdAsync(id);
                if (existingTeam == null)
                    return Result<Team?>.Failure("Team not found");
                if (existingTeam.captinId != capId)
                    return Result<Team?>.Failure("Only the team captain can update the team information");
                if (!string.IsNullOrEmpty(team.name))
                    existingTeam.name = team.name;
                if (!string.IsNullOrWhiteSpace(team.picUrl))
                    existingTeam.picUrl = team.picUrl;
                await _repository.UpdateAsync(existingTeam);
                return Result<Team?>.Success(existingTeam);
            }
            catch (Exception ex)
            {
                return Result<Team?>.Failure($"An error occurred while updating the team: {ex.Message}");
            }
        }

        public async Task<Result<bool>> DeleteAsync(Guid id,Guid capId)
        {
            try
            {
                var existingTeam = await _repository.GetByIdAsync(id);
                if (existingTeam == null)
                    return Result<bool>.Failure("Team not found");
                if (existingTeam.captinId != capId)
                    return Result<bool>.Failure("Only the team captain can delete the team");
                await _repository.DeleteAsync(id);
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"An error occurred while deleting the team: {ex.Message}");
            }
        }
        public async Task<Result<IEnumerable<Team>>> GetLeaderBoard(PaginationDto dto)
        {
            try
            {
                var teams = await _repository.LeaderBoard(dto.pageNumber, dto.pageSize);
                return Result<IEnumerable<Team>>.Success(teams);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<Team>>.Failure($"An error occurred while retrieving the leaderboard: {ex.Message}");
            }
            }

        public async Task<Result<IEnumerable<Team>>> SearchTeam(PaginationDto dto, string name)
        {
            try
            {
                var teams = await _repository.SearchTeam(dto.pageNumber, dto.pageSize, name);
                return Result<IEnumerable<Team>>.Success(teams);

            }
            catch (Exception ex)
            {
                return Result<IEnumerable<Team>>.Failure($"An error occurred while searching for teams: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<Team>>> UpdateStatsAsync(UpdateTeamStatsDto dto)
        {
            try
            {
                var homeTeam = await _repository.GetByIdAsync(dto.homeId);
                var awayTeam = await _repository.GetByIdAsync(dto.awayId);
                if (homeTeam == null || awayTeam == null)
                    return Result<IEnumerable<Team>>.Failure("Team not found");
                homeTeam.matchCount++;
                awayTeam.matchCount++;
                
                homeTeam.goals += dto.homeScore;
                homeTeam.goalsAgainest += dto.awayScore;

                awayTeam.goals += dto.awayScore;
                awayTeam.goalsAgainest += dto.homeScore;

                if (dto.homeScore > dto.awayScore)
                {
                    homeTeam.wins++;
                    homeTeam.points += 3;
                    awayTeam.loses++;
                }
                else if(dto.homeScore == dto.awayScore)
                {
                    homeTeam.points += 1;
                    awayTeam.points += 1;
                }else
                {
                    homeTeam.loses ++;
                    awayTeam.wins++;
                    awayTeam.points += 3;
                }
                await _repository.UpdateAsync(homeTeam);
                await _repository.UpdateAsync(awayTeam);
                await RecalculateRanksAsync();
                return Result<IEnumerable<Team>>.Success([homeTeam,awayTeam]);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<Team>>.Failure($"An error occurred while updating the team stats: {ex.Message}");
            }
        }
        private async Task RecalculateRanksAsync()
        {
            var teams = (await _repository.GetAllAsync())
                .Where(t => t.matchCount > 0)
                .OrderByDescending(t => t.points)
                .ThenByDescending(t => t.goals - t.goalsAgainest)
                .ThenByDescending(t => t.goals)
                .ToList();

            for (int i = 0; i < teams.Count; i++)
            {
                teams[i].Rank = i + 1;
                await _repository.UpdateAsync(teams[i]);
            }
        }
    }
}

