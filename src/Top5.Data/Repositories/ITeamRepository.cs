using System;
using System.Collections.Generic;
using System.Text;
using Top5.Contracts.Helper;
using Top5.Domain.Entities;

namespace Top5.Data.Repositories
{
    public interface ITeamRepository:IRepository<Team>
    {
        Task<Team?> GetByIdViewAsync(Guid id);
        Task<IEnumerable<Team?>> GetTeamsViewAsync();
        Task<PaginationResponse<Team>> SearchTeam(int pageNumber, int pageSize, string name);
        Task<PaginationResponse<Team>> LeaderBoard(int pageNumber, int pageSize);

    }
}
