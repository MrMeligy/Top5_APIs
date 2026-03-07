using System;
using System.Collections.Generic;
using System.Text;
using Top5.Data.Projections;
using Top5.Domain.Models;

namespace Top5.Data.Repositories
{
    public interface IPlayerRepository : IRepository<Player>
    {
        Task<PlayerDetailsProjection> GetPlayerDtoAsync(Guid id);
        Task<Player?> getByUserName(string userName);
        Task<bool> isExistAsync(Player player);
        Task<IEnumerable<Player>> SearchPlayersAsync(string userName);

    }
}
