using ControleBM.Domain.Entities;
using ControleBM.Infrastructure.Data;
using ControleBM.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleBM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ClienteResponseDto>>> GetClientes()
        {
            var clientes = await _context.Clientes
                .Select(c => new ClienteResponseDto
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Telefone = c.Telefone,
                    SaldoDevedor = c.SaldoDevedor
                })
                .ToListAsync();

            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteResponseDto>> GetClientesById(Guid id)
        {
            var cliente = await _context.Clientes
                .Where(c => c.Id == id && c.Ativo)
                .Select(c => new ClienteResponseDto
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Telefone = c.Telefone,
                    SaldoDevedor = c.SaldoDevedor
                })
                .FirstOrDefaultAsync();

            if (cliente == null)
                return NotFound(new { message = "Cliente não encontrado" });

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult<ClienteResponseDto>> CreateCliente(ClienteResponseDto request)
        {
            var cliente = new Cliente
            {
                Nome = request.Nome,
                Telefone = request.Telefone,
                SaldoDevedor = request.SaldoDevedor
            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            var response = new ClienteResponseDto
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Telefone = cliente.Telefone,
                SaldoDevedor = cliente.SaldoDevedor
            };

            return CreatedAtAction(nameof(GetClientesById), new { id = cliente.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCliente(Guid id, [FromBody] ClienteResponseDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
                return NotFound(new { message = "Cliente não encontrado" });

            cliente.Nome = dto.Nome;
            cliente.Telefone = dto.Telefone;
            cliente.SaldoDevedor = dto.SaldoDevedor;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(Guid id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
                return NotFound(new { message = "Cliente não encontrado" });

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
