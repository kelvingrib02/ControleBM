using ControleBM.Domain.Entities;
using ControleBM.Infrastructure.Data;
using ControleBM.Shared.DTOs;
using ControleBM.Shared.Enums;
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
        public async Task<ActionResult<List<ProdutoResponseDto>>> GetProdutos()
        {
            var produtos = await _context.Produtos
                .Where(p => p.Ativo)
                .Select(p => new ProdutoResponseDto
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Descricao = p.Descricao,
                    PrecoVenda = p.PrecoVenda,
                    CustoUnitario = p.CustoUnitario,
                    EstoqueAtual = p.EstoqueAtual,
                    Tipo = (TipoProduto)p.Tipo
                })
                .ToListAsync();

            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoResponseDto>> GetProdutoById(Guid id)
        {
            var produto = await _context.Produtos
                .Where(p => p.Id == id && p.Ativo)
                .Select(p => new ProdutoResponseDto
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Descricao = p.Descricao,
                    PrecoVenda = p.PrecoVenda,
                    CustoUnitario = p.CustoUnitario,
                    EstoqueAtual = p.EstoqueAtual,
                    Tipo = (TipoProduto)p.Tipo
                })
                .FirstOrDefaultAsync();

            if (produto == null)
                return NotFound(new { message = "Produto não encontrado" });

            return Ok(produto);
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoResponseDto>> CreateProduto(ProdutoRequestDto request)
        {
            var produto = new Produto
            {
                Nome = request.Nome,
                Descricao = request.Descricao,
                PrecoVenda = request.PrecoVenda,
                CustoUnitario = request.CustoUnitario,
                EstoqueAtual = request.EstoqueAtual,
                Tipo = request.Tipo,
                Ativo = true
            };

            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            var response = new ProdutoResponseDto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                PrecoVenda = produto.PrecoVenda,
                CustoUnitario = produto.CustoUnitario,
                EstoqueAtual = produto.EstoqueAtual,
                Tipo = produto.Tipo,
            };

            return CreatedAtAction(nameof(GetProdutoById), new { id = produto.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduto(Guid id, [FromBody] ProdutoRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null || !produto.Ativo)
                return NotFound(new { message = "Produto não encontrado" });

            produto.Nome = dto.Nome;
            produto.Descricao = dto.Descricao;
            produto.PrecoVenda = dto.PrecoVenda;
            produto.CustoUnitario = dto.CustoUnitario;
            produto.EstoqueAtual = dto.EstoqueAtual;
            produto.Tipo = dto.Tipo;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(Guid id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
                return NotFound(new { message = "Produto não encontrado" });

            produto.Ativo = false;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}