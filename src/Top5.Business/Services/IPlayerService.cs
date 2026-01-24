using Top5.Contracts.DTOs;
using Top5.Domain.Models;

namespace Top5.Business.Services
{
    public interface IPlayerService
    {
        Task<Player?> GetByIdAsync(Guid id);
        Task<PlayerDto> GetPlayerDtoById(Guid id);
        Task<IEnumerable<Player>> GetAllAsync();
        Task<Player> CreateAsync(Player player);
        Task<Player?> UpdateAsync(Guid id, Player player);
        Task<bool> DeleteAsync(Guid id);
    }
}

