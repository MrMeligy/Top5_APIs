using System;
using System.Collections.Generic;
using System.Text;
using Top5.Domain.Entities;

namespace Top5.Data.Repositories
{
    public interface ITeamPlayersRepository : IRepository<TeamPlayers>
    {
        public Task<IEnumerable<TeamPlayers>> GetTeamPlayersView(Guid teamId);
    }
}
