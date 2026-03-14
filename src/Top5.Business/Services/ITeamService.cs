using Top5.Business.Result;
using Top5.Contracts.DTOs;
using Top5.Domain.Entities;

namespace Top5.Business.Services
{
    public interface ITeamService
    {
        Task<Result<Team?>> GetByIdViewAsync(Guid id);
        //public Task<Result<IEnumerable<Team?>>> GetTeamsViewAsync();
        Task<Result<IEnumerable<Team>>> GetLeaderBoard(PaginationDto dto);
        Task<Result<IEnumerable<Team>>> SearchTeam(PaginationDto dto,string name);
        Task<Result<Team>> CreateAsync(CreateTeamDto team,Guid capId);
        Task<Result<Team?>> UpdateInfoAsync(Guid id,Guid capId, UpdateTeamInfoDto team);
        Task<Result<IEnumerable<Team>>> UpdateStatsAsync(UpdateTeamStatsDto dto);
        Task<Result<bool>> DeleteAsync(Guid id,Guid capId);

    }
}

