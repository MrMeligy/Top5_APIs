using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Top5.Business.Services;
using Top5.Contracts.DTOs;
using Top5.Domain.Entities;

namespace Top5.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReservationController : BaseController
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService service)
        {
            _reservationService = service;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReservationByDay(DateTime date)
        {
            var result = await _reservationService.GetReservationsByDayAsync(date);
            if (!result.IsSuccess)
            {
                return Failed(result.Error ?? "An error occurred while retrieving Reservations.", StatusCodes.Status500InternalServerError);
            }
            return Success(result);
        }
        [HttpGet("PlayerReservations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReservationByPlayer(int pageSize,int pageNumber )
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var id = Guid.Parse(userId!);
            var result = await _reservationService.GetPlayerReservationsAsync(id,pageSize,pageNumber);
            if (!result.IsSuccess)
            {
                return Failed(result.Error ?? "An error occurred while retrieving Reservations.", StatusCodes.Status500InternalServerError);
            }
            return Success(result);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddReservation([FromBody] CreateReservationDto reservation)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var id = Guid.Parse(userId!);
            var result = await _reservationService.CreateReservationAsync(reservation,id);
            if (!result.IsSuccess)
            {
                return Failed(result.Error ?? "An error occurred while Create Reservations.", StatusCodes.Status500InternalServerError);
            }
            return Success(result);
        }

    }
}
