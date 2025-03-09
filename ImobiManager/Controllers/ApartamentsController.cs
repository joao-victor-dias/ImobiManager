using ImobiManager.Data;
using ImobiManager.DTO;
using ImobiManager.Entities;
using ImobiManager.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ImobiManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartamentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApartamentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Apartament>>> GetApartaments()
        {
            return await _context.Apartaments.ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Apartament>> GetApartament(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartament = await _context.Apartaments.FindAsync(id);

            if (apartament == null)
            {
                return NotFound();
            }

            return apartament;
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Apartament>> PostApartament([FromBody] ApartamentDto apartamentDto)
        {
            if (apartamentDto == null)
                return BadRequest("Dados inválidos.");

            var apartament = new Apartament
            {
                Number = apartamentDto.Number,
                BlockOrTower = apartamentDto.BlockOrTower,
                Floor = apartamentDto.Floor,
                Area = apartamentDto.Area,
                Bedrooms = apartamentDto.Bedrooms,
                Bathrooms = apartamentDto.Bathrooms,
                GarageSpaces = apartamentDto.GarageSpaces,
                Price = apartamentDto.Price,
                Address = apartamentDto.Address,
                Status = apartamentDto.Status,
                Description = apartamentDto.Description
            };

            _context.Apartaments.Add(apartament);
            await _context.SaveChangesAsync();

            var apartamentWithDetails = await _context.Apartaments
                .Include(a => a.Reservation)
                .Include(a => a.Sale)
                .FirstOrDefaultAsync(a => a.Id == apartament.Id);

            return CreatedAtAction("GetApartament", new { id = apartament.Id }, apartamentWithDetails);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutApartament(int id, ApartamentDto apartamentDto)
        {
            var apartament = await _context.Apartaments.FindAsync(id);

            if (apartament == null)
            {
                return NotFound();
            }

            apartament.Number = apartamentDto.Number;
            apartament.BlockOrTower = apartamentDto.BlockOrTower;
            apartament.Floor = apartamentDto.Floor;
            apartament.Area = apartamentDto.Area;
            apartament.Bedrooms = apartamentDto.Bedrooms;
            apartament.Bathrooms = apartamentDto.Bathrooms;
            apartament.GarageSpaces = apartamentDto.GarageSpaces;
            apartament.Price = apartamentDto.Price;
            apartament.Address = apartamentDto.Address;
            apartament.Status = apartamentDto.Status;
            apartament.Description = apartamentDto.Description;

            _context.Entry(apartament).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Apartament>> DeleteApartament(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartament = await _context.Apartaments.FindAsync(id);

            if (apartament == null)
            {
                return NotFound();
            }

            _context.Apartaments.Remove(apartament);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Apartamento excluído com sucesso." });
        }
    }
}
