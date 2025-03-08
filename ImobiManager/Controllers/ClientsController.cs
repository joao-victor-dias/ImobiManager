using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ImobiManager.Data;
using ImobiManager.Entities;
using ImobiManager.DTO;

namespace ImobiManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            return await _context.Clients.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

       
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient([FromBody] ClientDto clientDto)
        {
            if (clientDto == null)
                return BadRequest("Dados inválidos.");

            var client = new Client
            {
                Name = clientDto.Name,
                CpfCnpj = clientDto.CpfCnpj,
                IsCompany = clientDto.IsCompany,
                DateOfBirth = clientDto.DateOfBirth,
                Email = clientDto.Email,
                Phone = clientDto.Phone,
                Address = clientDto.Address
            };

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClient", new { id = client.Id }, client);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(int id, ClientDto clientDto)
        {
            var client = new Client
            {
                Name = clientDto.Name,
                CpfCnpj = clientDto.CpfCnpj,
                IsCompany = clientDto.IsCompany,
                DateOfBirth = clientDto.DateOfBirth,
                Email = clientDto.Email,
                Phone = clientDto.Phone,
                Address = clientDto.Address
            };

            if (id != client.Id)
            {
                return BadRequest();
            }

            _context.Entry(client).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Client>> DeleteClient(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Cliente excluído com sucesso."});
        }
    }
}