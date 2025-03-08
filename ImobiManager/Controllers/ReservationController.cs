using ImobiManager.Data;
using ImobiManager.DTO;
using ImobiManager.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ImobiManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReservationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
            return await _context.Reservations
                .Include(r => r.Client)
                .Include(r => r.Apartament)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Reservation>> GetReservation(int id)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Client)
                .Include(r => r.Apartament)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
            {
                return NotFound();
            }

            return reservation;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Reservation>> PostReservation(ReservationDto reservationDto)
        {
            var clientExists = await _context.Clients.AnyAsync(c => c.Id == reservationDto.ClientId);
            var apartamentExists = await _context.Apartaments.AnyAsync(a => a.Id == reservationDto.ApartamentId);

            if (!clientExists || !apartamentExists)
            {
                return BadRequest("Cliente ou Apartamento não encontrado.");
            }

            var reservation = new Reservation
            {
                ClientId = reservationDto.ClientId,
                ApartamentId = reservationDto.ApartamentId,
                ReservationDate = reservationDto.ReservationDate
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservation", new { id = reservation.Id }, reservation);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutReservation(int id, ReservationDto reservationDto)
        {
            var existingReservation = await _context.Reservations.FindAsync(id);
            if (existingReservation == null)
            {
                return NotFound();
            }

            var clientExists = await _context.Clients.AnyAsync(c => c.Id == reservationDto.ClientId);
            var apartamentExists = await _context.Apartaments.AnyAsync(a => a.Id == reservationDto.ApartamentId);

            if (!clientExists || !apartamentExists)
            {
                return BadRequest("Cliente ou Apartamento não encontrado.");
            }

            existingReservation.ClientId = reservationDto.ClientId;
            existingReservation.ApartamentId = reservationDto.ApartamentId;
            existingReservation.ReservationDate = reservationDto.ReservationDate;

            _context.Entry(existingReservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Reservations.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
