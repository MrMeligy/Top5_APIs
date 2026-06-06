using System;
using System.Collections.Generic;
using System.Text;
using Top5.Business.Result;
using Top5.Contracts.Helper;
using Top5.Data.Repositories;
using Top5.Domain.Entities;

namespace Top5.Business.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationService _reservationService;
        private readonly IReservationRepository _repo;
        private static readonly SemaphoreSlim _rankLock = new SemaphoreSlim(1, 1);

        public ReservationService(IReservationService reservationService, IReservationRepository reservationRepository)
        {
            _reservationService = reservationService;
            _repo = reservationRepository;
        }

        public async Task<Result<Reservation>> CreateReservationAsync(Reservation reservation)
        {
            await using var transaction = await _repo.BeginTransactionAsync();
            await _rankLock.WaitAsync();
            try
            {
                bool hasConflict = await _repo.IsHasConflict(
                    reservation.PitchId,
                    reservation.From,
                    reservation.To);

                if (hasConflict)
                    return Result<Reservation>.Failure("The reservation conflicts with an existing reservation.");

                await _repo.AddAsync(reservation);
                await _repo.SaveChangesAsync();
                await transaction.CommitAsync();

                return Result<Reservation>.Success(reservation);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Result<Reservation>.Failure(ex.Message);
            }
            finally
            {
                _rankLock.Release();
            }
        }

        public async Task<Result<PaginationResponse<Reservation>>> GetPlayerReservationsAsync(Guid playerId, int pageSize, int pageNumber)
        {
            try
            {
                var result = await _repo.GetPlayerReservations(playerId, pageSize, pageNumber);
                return Result<PaginationResponse<Reservation>>.Success(result);
            }
            catch (Exception ex)
            {
                return Result<PaginationResponse<Reservation>>.Failure(ex.Message);
            }
        }

        public async Task<Result<IEnumerable<Reservation>>> GetReservationsByDayAsync(DateTime day)
        {
            try
            {
                var result = await _repo.GetReservationsByDay(day);
                return Result<IEnumerable<Reservation>>.Success(result);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<Reservation>>.Failure(ex.Message);
            }
        }
    }
}
