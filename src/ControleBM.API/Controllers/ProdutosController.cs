using ControleBM.Domain.Entities;
using ControleBM.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleBM.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Produto>>> GetProdutos()
        {
            return await _context.Produtos.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
        {
            if (produto.CustoUnitario <= 0)
                return BadRequest("O preço de venda deve ser maior que zero.");

            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProdutos), new { id = produto.Id }, produto);
        }
    }
}
