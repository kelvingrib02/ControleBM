using ControleBM.Shared.DTOs;

namespace ControleBM.Frontend.Services
{
    public interface IProdutoService
    {
        Task<List<ProdutoResponseDto>> GetProdutosAsync();
    }
}