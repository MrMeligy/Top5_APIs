using System;
using System.Collections.Generic;
using System.Text;
using Top5.Business.Result;
using Top5.Contracts.DTOs;
using Top5.Contracts.Helper;
using Top5.Data.Repositories;
using Top5.Domain.Entities;
using Top5.Domain.Enums;

namespace Top5.Business.Services
{
    public class StatesService : IStatesService
    {
        private readonly IStatesRepository _repo;

        public StatesService(IStatesRepository statesRepository)
        {
            _repo= statesRepository;
        }

        public async Task<Result<PaginationResponse<PlayerStatesRankDto>>> GetAssistsRanking(int pageSize, int pageNumber)
        {
            try
            {
                var ranking = await _repo.GetAssists(pageSize, pageNumber);
                return Result<PaginationResponse<PlayerStatesRankDto>>.Success(ranking);
            }
            catch(Exception er) {
                return Result<PaginationResponse<PlayerStatesRankDto>>.Failure(er.Message);
            }
        }

        public async Task<Result<PaginationResponse<PlayerStatesRankDto>?>> GetPlayerRankAsync(Guid playerId, RankType rankType)
        {
            try
            {
                var ranking = await _repo.GetPlayerRankAsync(playerId, rankType);
                if (ranking == null)
                {
                    return Result<PaginationResponse<PlayerStatesRankDto>?>.Failure("Player Not Found");
                }
                return Result<PaginationResponse<PlayerStatesRankDto>?>.Success(ranking);
            }
            catch(Exception er) {
                return Result<PaginationResponse<PlayerStatesRankDto>?>.Failure(er.Message);
            }
        }

        public async Task<Result<PaginationResponse<PlayerStatesRankDto>>> GetSavesRanking(int pageSize, int pageNumber)
        {
            try
            {
                var ranking = await _repo.GetSaves(pageSize, pageNumber);
                return Result<PaginationResponse<PlayerStatesRankDto>>.Success(ranking);
            }
            catch(Exception er) {
                return Result<PaginationResponse<PlayerStatesRankDto>>.Failure(er.Message);
            }
        }

        public async Task<Result<PaginationResponse<PlayerStatesRankDto>>> GetScorersRanking(int pageSize, int pageNumber)
        {
            try
            {
                var ranking = await _repo.GetScorers(pageSize, pageNumber);
                return Result<PaginationResponse<PlayerStatesRankDto>>.Success(ranking);
            }
            catch(Exception er) {
                return Result<PaginationResponse<PlayerStatesRankDto>>.Failure(er.Message);
            }
        }
    }
}
