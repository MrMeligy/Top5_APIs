using System;
using System.Collections.Generic;
using System.Text;
using Top5.Domain.Entities;

namespace Top5.Data.Repositories
{
    public interface IMatchPlayerRepository : IRepository<MatchPlayers>
    {
        public Task<MatchPlayers?> GetByMatchPlayerViewAsync(Guid id);
        public Task<IEnumerable<MatchPlayers>?> GetByPlayerMatchesAsync(Guid playerId);
        public Task<IEnumerable<MatchPlayers>?> GetByMatchPlayersViewAsync(Guid matchId);
        public Task<MatchPlayers?> GetMatchAndPlayerAsync(Guid matchId,Guid playerId);
        public Task<IEnumerable<MatchPlayers>?> GetPlayerTeamStatsAsync(Guid teamId, Guid playerId);

    }
}
