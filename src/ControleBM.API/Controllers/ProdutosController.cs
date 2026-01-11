using ControleBM.Domain.Entities;
using ControleBM.Domain.Enums;
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
                .Select(p => new ProdutoResponseDto
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Descricao = p.Descricao,
                    PrecoVenda = p.PrecoVenda,
                    PrecoCusto = p.PrecoCusto,
                    QuantidadeEstoque = p.QuantidadeEstoque,
                    Tipo = (TipoProduto)p.Tipo
                })
                .ToListAsync();

            return Ok(produtos);
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoResponseDto>> CreateProduto(ProdutoRequestDto request)
        {
            var produto = new Produto
            {
                Nome = request.Nome,
                Descricao = request.Descricao,
                PrecoVenda = request.PrecoVenda,
                PrecoCusto = request.PrecoCusto,
                QuantidadeEstoque = request.QuantidadeEstoque,
                Tipo = (int)request.Tipo
            };

            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            var response = new ProdutoResponseDto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                PrecoVenda = produto.PrecoVenda,
                PrecoCusto = produto.PrecoCusto,
                QuantidadeEstoque = produto.QuantidadeEstoque,
                Tipo = request.Tipo
            };

            return CreatedAtAction(nameof(GetProdutos), new { id = produto.Id }, response);
        }
    }
}