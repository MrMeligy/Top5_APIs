using System;
using System.Collections.Generic;
using System.Text;
using Top5.Contracts.Helper;
using Top5.Data.Projections;
using Top5.Domain.Models;

namespace Top5.Data.Repositories
{
    public interface IPlayerRepository : IRepository<Player>
    {
        Task<PlayerDetailsProjection> GetPlayerDtoAsync(Guid id);
        Task<Player?> getByUserName(string userName);
        Task<Guid?> isPhoneExist(string phone);
        Task<Guid?> isUserNameExist(string username);
        Task<bool> isExistAsync(Player player);
        Task<PaginationResponse<Player>> SearchPlayersAsync(string userName,int pageSize,int pageNumber);

    }
}
