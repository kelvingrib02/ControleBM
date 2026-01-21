using System.Net.Http.Json;
using ControleBM.Shared.DTOs;

namespace ControleBM.Frontend.Services
{
    public interface IProdutoService
    {
        Task<List<ProdutoResponseDto>> GetProdutosAsync();
        Task<ProdutoResponseDto?> GetProdutoByIdAsync(Guid id);
        Task AddProdutoAsync(ProdutoRequestDto produto);
        Task UpdateProdutoAsync(Guid id, ProdutoRequestDto produto);
        Task DeleteProdutoAsync(Guid id);
    }
    public class ProdutoService : IProdutoService
    {
        private readonly HttpClient _http;

        public ProdutoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ProdutoResponseDto>> GetProdutosAsync()
        {
            var resultado = await _http.GetFromJsonAsync<List<ProdutoResponseDto>>("api/produtos");
            return resultado ?? new List<ProdutoResponseDto>();
        }

        public async Task<ProdutoResponseDto?> GetProdutoByIdAsync(Guid id)
        {
            return await _http.GetFromJsonAsync<ProdutoResponseDto>($"api/produtos/{id}");
        }

        public async Task AddProdutoAsync(ProdutoRequestDto produto)
        {
            await _http.PostAsJsonAsync("api/produtos", produto);
        }

        public async Task UpdateProdutoAsync(Guid id, ProdutoRequestDto produto)
        {
            var response = await _http.PutAsJsonAsync($"api/produtos/{id}", produto);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteProdutoAsync(Guid id)
        {
            var response = await _http.DeleteAsync($"api/produtos/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}