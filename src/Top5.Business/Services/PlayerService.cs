using Top5.Business.Result;
using Top5.Contracts.DTOs;
using Top5.Data.Repositories;
using Top5.Domain.Models;

namespace Top5.Business.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _repository;

        public PlayerService(IPlayerRepository repository)
        {
            _repository = repository;
        }
        public async Task<Result<PlayerDto?>> GetPlayerDtoById(Guid id)
        {
            var p = await _repository.GetPlayerDtoAsync(id);
            if (p == null)
                return Result<PlayerDto?>.Failure("Player not found");
            var player = new PlayerDto
            {
                id = p.id,
                username = p.username,
                picUrl = p.picUrl,
                position = p.position,
                dob = p.dob,
                phone = p.phone,
                gender = p.gender,
                level = p.level,
                goals = p.goals,
                assists = p.assists,
                saves = p.saves,
                matchCount = p.matchCount,
                rate = p.rate,
                team = p.team
            };
            return Result<PlayerDto?>.Success(player);

        }
        public async Task<Result<Player?>> GetByIdAsync(Guid id)
        {
            var p = await _repository.GetByIdAsync(id);
            if (p == null)
                return Result<Player?>.Failure("Player not found");
            return Result<Player?>.Success(p);
        }

        public async Task<Result<IEnumerable<Player>>> GetAllAsync()
        {
            var p = await _repository.GetAllAsync();
            return Result<IEnumerable<Player>>.Success(p);
        }

        //registeration is handled by auth service, so we don't need this method in player service
        //public async Task<Result<Player>> CreateAsync(Player player)
        //{
        //    if (player.id == Guid.Empty)
        //    {
        //        player.id = Guid.NewGuid();
        //    }
        //    return await _repository.AddAsync(player);
        //}

        public async Task<Result<Player>> UpdateAsync(Guid id, Player player)
        {
            var existingPlayer = await _repository.GetByIdAsync(id);
            if (existingPlayer == null)
                return Result<Player>.Failure("Player Not Exist");

            player.id = id;
            try
            {
                var p = await _repository.UpdateAsync(player);
                return Result<Player>.Success(p);
            }
            catch (Exception ex)
            {
                return Result<Player>.Failure(ex.Message);
            }
        }

        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            if (await _repository.GetByIdAsync(id) == null)
                return Result<bool>.Failure("Player Not Exist");
            try
            {
                var r = await _repository.DeleteAsync(id);
                return Result<bool>.Success(r);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
        }
        public async Task<Result<IEnumerable<Player>>> SearchPlayersAsync(string userName)
        {
            try
            {
                var result = await _repository.SearchPlayersAsync(userName);
                return Result<IEnumerable<Player>>.Success(result);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<Player>>.Failure(ex.Message);
            }
        }
    }
}

