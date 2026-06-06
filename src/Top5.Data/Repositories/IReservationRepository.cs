using System;
using System.Collections.Generic;
using System.Text;
using Top5.Contracts.Helper;
using Top5.Domain.Entities;

namespace Top5.Data.Repositories
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        Task<IEnumerable<Reservation>> GetReservationsByDay(DateTime day);
        Task<PaginationResponse<Reservation>> GetPlayerReservations(Guid playerId, int pageSize, int pageNumber);
        Task<bool> IsHasConflict(int pitchId, DateTime startTime, DateTime endTime);

    }
}
