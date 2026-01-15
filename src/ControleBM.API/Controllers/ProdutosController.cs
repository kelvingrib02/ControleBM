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
                .Select(p => new ProdutoResponseDto
                {
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
                Tipo = (TipoProduto)request.Tipo,
            };

            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            var response = new ProdutoResponseDto
            {
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                PrecoVenda = produto.PrecoVenda,
                CustoUnitario = produto.CustoUnitario,
                EstoqueAtual = produto.EstoqueAtual,
                Tipo = (TipoProduto)produto.Tipo,
            };

            return CreatedAtAction(nameof(GetProdutos), new { id = produto.Id }, response);
        }
    }
}