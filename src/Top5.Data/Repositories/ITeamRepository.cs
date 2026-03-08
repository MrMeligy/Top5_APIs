using System;
using System.Collections.Generic;
using System.Text;
using Top5.Domain.Entities;

namespace Top5.Data.Repositories
{
    public interface ITeamRepository:IRepository<Team>
    {
        Task<Team?> GetByIdViewAsync(Guid id);
        Task<IEnumerable<Team?>> GetTeamsViewAsync();
        Task<IEnumerable<Team>> SearchTeam(int pageNumber, int pageSize, string name);
        Task<IEnumerable<Team>> LeaderBoard(int pageNumber, int pageSize);

    }
}
