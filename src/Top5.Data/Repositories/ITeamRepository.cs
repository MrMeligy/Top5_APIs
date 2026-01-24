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
    }
}
