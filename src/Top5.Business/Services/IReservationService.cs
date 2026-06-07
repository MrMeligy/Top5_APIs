using System;
using System.Collections.Generic;
using System.Text;
using Top5.Business.Result;
using Top5.Contracts.DTOs;
using Top5.Contracts.Helper;
using Top5.Domain.Entities;

namespace Top5.Business.Services
{
    public interface IReservationService
    {
        Task<Result<Reservation>> CreateReservationAsync(CreateReservationDto reservation,Guid playerId);  
        Task<Result<PaginationResponse<Reservation>>> GetPlayerReservationsAsync(Guid playerId, int pageSize, int pageNumber);
        Task<Result<IEnumerable<Reservation>>> GetReservationsByDayAsync(DateTime day);
    }
}
