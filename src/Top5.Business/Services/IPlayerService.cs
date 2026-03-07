using Top5.Business.Result;
using Top5.Contracts.DTOs;
using Top5.Domain.Models;

namespace Top5.Business.Services
{
    public interface IPlayerService
    {
        Task<Result<Player?>> GetByIdAsync(Guid id);
        Task<Result<PlayerDto?>> GetPlayerDtoById(Guid id);
        Task<Result<IEnumerable<Player>>> GetAllAsync();
        Task<Result<IEnumerable<Player>>> SearchPlayersAsync(string userName);
        //Task<Result<IEnumerable<Player>>> GetTopPlayersAsync();

        //Task<Result<Player>> CreateAsync(Player player);
        Task<Result<Player>> UpdateAsync(Guid id, Player player);
        Task<Result<bool>> DeleteAsync(Guid id);
    }
}

