using ImobiManager.Data;
using ImobiManager.DTO;
using ImobiManager.Entities;
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
        public async Task<ActionResult<IEnumerable<Sale>>> GetSales()
        {
            return await _context.Sales.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sale>> GetSale(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales.FindAsync(id);

            if (sale == null)
            {
                return NotFound();
            }

            return sale;
        }

        [HttpPost]
        public async Task<ActionResult<Sale>> PostSale(SaleDto saleDto)
        {
            var clientExists = await _context.Clients.AnyAsync(c => c.Id == saleDto.ClientId);
            var apartamentExists = await _context.Apartaments.AnyAsync(a => a.Id == saleDto.ApartamentId);

            if (!clientExists || !apartamentExists)
            {
                return BadRequest("Cliente ou Apartamento não encontrado.");
            }

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
        public async Task<IActionResult> DeleteSale(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
