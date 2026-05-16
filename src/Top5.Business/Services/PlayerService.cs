using Top5.Business.Result;
using Top5.Contracts.DTOs;
using Top5.Contracts.Helper;
using Top5.Data.Repositories;
using Top5.Domain.Entities;
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
            try
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
            catch (Exception ex)
            {
                return Result<PlayerDto?>.Failure(ex.Message);
            }

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

        public async Task<Result<Player>> UpdateAsync(Guid id, UpdatePlayerDto player)
        {
            var existingPlayer = await _repository.GetByIdAsync(id);
            if (existingPlayer == null)
                return Result<Player>.Failure("Player Not Exist");

            var phoneExists = await _repository.isPhoneExist(player.phone);
            var userNameExists = await _repository.isUserNameExist(player.username);
            
            if (phoneExists != null && phoneExists != id)
            {
                return Result<Player>.Failure("this phone already registerd with another player");
            }
            if (userNameExists != null && userNameExists != id)
            {
                return Result<Player>.Failure("this username unavailble");
            }
            existingPlayer.username = player.username;
            existingPlayer.phone = player.phone;
            existingPlayer.position = player.position;
            existingPlayer.level = player.level;
            if (!string.IsNullOrWhiteSpace(player.picUrl))
            {
                existingPlayer.picUrl = player.picUrl;
            }
            try
            {
                var p = await _repository.UpdateAsync(existingPlayer);
                await _repository.SaveChangesAsync();
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
                await _repository.SaveChangesAsync();
                return Result<bool>.Success(r);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
        }
        public async Task<Result<PaginationResponse<Player>>> SearchPlayersAsync(string userName, int pageSize, int pageNumber)
        {
            try
            {
                var result = await _repository.SearchPlayersAsync(userName,pageSize,pageNumber);
                return Result<PaginationResponse<Player>>.Success(result);
            }
            catch (Exception ex)
            {
                return Result<PaginationResponse<Player>>.Failure(ex.Message);
            }
        }
    }
}

