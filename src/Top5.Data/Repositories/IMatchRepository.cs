using System;
using System.Collections.Generic;
using System.Text;
using Top5.Domain.Entities;

namespace Top5.Data.Repositories
{
    public interface IMatchRepository : IRepository<Match> 
    {
        Task<IEnumerable<Match>> GetAllWithTeamsAsync();
        Task<Match?> GetByIdWithTeamsAsync(Guid id);
    }
}
