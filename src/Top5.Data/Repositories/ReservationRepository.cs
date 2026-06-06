using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Top5.Contracts.Helper;
using Top5.Domain.Entities;
using Top5.Domain.Enums;

namespace Top5.Data.Repositories
{
    public class ReservationRepository : Repository<Reservation>, IReservationRepository
    {
        public ReservationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> IsHasConflict(int pitchId,DateTime startTime, DateTime endTime)
        {
            bool hasConflict = await _context.Reservations
            .AnyAsync(r =>
            r.PitchId == pitchId &&
            r.From < endTime &&
            r.To > startTime);

            return hasConflict;
        }

        async Task<PaginationResponse<Reservation>> IReservationRepository.GetPlayerReservations(Guid playerId, int pageSize, int pageNumber)
        {
            var query = _dbSet
                .Where(r => r.PlayerId == playerId);

            var totalCount = await query.CountAsync();

            var reservations = await query
                .AsNoTracking()
                .OrderBy(r => r.From)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(r => r.Player)
                .Include(r => r.Pitch)
                .ToListAsync();

            return new PaginationResponse<Reservation>
            {
                Data = reservations,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
            
        }

        async Task<IEnumerable<Reservation>> IReservationRepository.GetReservationsByDay(DateTime day)
        {
            return await _dbSet
                .Where(r => r.From.Date == day.Date)
                .Include(r => r.Player)
                .Include(r => r.Pitch)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
