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
    public class SalesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Sale>>> GetSales()
        {
            var sales = await _context.Sales
                .Include(s => s.Client)   
                .Include(s => s.Apartament) 
                .ToListAsync();

            return sales;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Sale>> GetSale(int id)
        {
            var sale = await _context.Sales
                .Include(s => s.Client)    
                .Include(s => s.Apartament) 
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sale == null)
            {
                return NotFound();
            }

            return sale;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Sale>> PostSale(SaleDto saleDto)
        {
            var clientExists = await _context.Clients.AnyAsync(c => c.Id == saleDto.ClientId);
            var apartamentExists = await _context.Apartaments.AnyAsync(a => a.Id == saleDto.ApartamentId);

            if (!clientExists || !apartamentExists)
            {
                return BadRequest("Cliente ou Apartamento não encontrado.");
            }

            var existingSale = await _context.Sales
                .FirstOrDefaultAsync(s => s.ApartamentId == saleDto.ApartamentId);

            if (existingSale != null)
            {
                return BadRequest("Este apartamento já foi vendido.");
            }

            var apartament = await _context.Apartaments.FindAsync(saleDto.ApartamentId);

            if (apartament == null)
            {
                return BadRequest("Apartamento não encontrado.");
            }

            apartament.Status = (Enums.ApartmentStatus)2;  
            _context.Entry(apartament).State = EntityState.Modified;

            var sale = new Sale
            {
                ClientId = saleDto.ClientId,
                ApartamentId = saleDto.ApartamentId,
                SaleDate = saleDto.SaleDate
            };

            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSale", new { id = sale.Id }, sale);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutSale(int id, SaleDto saleDto)
        {
            var existingSale = await _context.Sales.FindAsync(id);
            if (existingSale == null)
            {
                return NotFound();
            }

            var clientExists = await _context.Clients.AnyAsync(c => c.Id == saleDto.ClientId);
            var apartamentExists = await _context.Apartaments.AnyAsync(a => a.Id == saleDto.ApartamentId);

            if (!clientExists || !apartamentExists)
            {
                return BadRequest("Cliente ou Apartamento não encontrado.");
            }

            existingSale.ClientId = saleDto.ClientId;
            existingSale.ApartamentId = saleDto.ApartamentId;
            existingSale.SaleDate = saleDto.SaleDate;

            var existingReservation = await _context.Reservations
                .FirstOrDefaultAsync(r => r.ApartamentId == saleDto.ApartamentId);

            var currentApartament = await _context.Apartaments.FindAsync(saleDto.ApartamentId);
            if (currentApartament != null)
            {
                if (existingReservation != null)
                {
                    currentApartament.Status = Enums.ApartmentStatus.Reserved;
                }
                else
                {
                    currentApartament.Status = Enums.ApartmentStatus.Available;
                }

                _context.Entry(currentApartament).State = EntityState.Modified;
            }

            var newApartament = await _context.Apartaments.FindAsync(saleDto.ApartamentId);
            if (newApartament != null)
            {
                newApartament.Status = Enums.ApartmentStatus.Sold;
                _context.Entry(newApartament).State = EntityState.Modified;
            }

            _context.Entry(existingSale).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Sales.Any(e => e.Id == id))
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
        public async Task<IActionResult> DeleteSale(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }

            var apartment = await _context.Apartaments.FindAsync(sale.ApartamentId);
            if (apartment != null)
            {
                var existingReservation = await _context.Reservations
                    .FirstOrDefaultAsync(r => r.ApartamentId == sale.ApartamentId);

                if (existingReservation != null)
                {
                    apartment.Status = Enums.ApartmentStatus.Reserved; 
                }
                else
                {
                    apartment.Status = Enums.ApartmentStatus.Available; 
                }

                _context.Entry(apartment).State = EntityState.Modified;
            }

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Venda excluída com sucesso." });
        }
    }
}
