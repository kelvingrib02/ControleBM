using System.Net.Http.Json;
using ControleBM.Shared.DTOs;

namespace ControleBM.Frontend.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly HttpClient _http;

        public ProdutoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ProdutoResponseDto>> GetProdutosAsync()
        {
            var produtos = await _http.GetFromJsonAsync<List<ProdutoResponseDto>>("api/produtos");
            return produtos ?? new List<ProdutoResponseDto>();
        }
    }
}