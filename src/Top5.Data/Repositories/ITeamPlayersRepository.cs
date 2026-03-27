using System;
using System.Collections.Generic;
using System.Text;
using Top5.Domain.Entities;

namespace Top5.Data.Repositories
{
    public interface ITeamPlayersRepository : IRepository<TeamPlayers>
    {
        Task<IEnumerable<TeamPlayers>> GetTeamPlayers(Guid teamId);
        Task<IEnumerable<TeamPlayers>> GetPlayerTeams(Guid playerId);
        Task<TeamPlayers?> ExitTeam(Guid teamId, Guid playerId);
        Task<bool> IsAtTeam(Guid teamId, Guid playerId);
    }
}
