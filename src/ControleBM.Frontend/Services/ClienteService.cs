using System.Net.Http.Json;
using ControleBM.Shared.DTOs;

namespace ControleBM.Frontend.Services
{
    public interface IClienteService
    {
        Task<List<ClienteResponseDto>> GetClienteAsync();
        Task<ClienteResponseDto?> GetClienteByIdAsync(Guid id);
        //Task CreateProdutoAsync(ProdutoRequestDto produto);
        //Task UpdateProdutoAsync(Guid id, ProdutoRequestDto produto);
        //Task DeleteProdutoAsync(Guid id);
    }
    public class ClienteService : IClienteService
    {
        private readonly HttpClient _http;

        public ClienteService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ClienteResponseDto>> GetClienteAsync()
        {
            var resultado = await _http.GetFromJsonAsync<List<ClienteResponseDto>>("api/clientes");
            return resultado ?? new List<ClienteResponseDto>();
        }

        public async Task<ClienteResponseDto?> GetClienteByIdAsync(Guid id)
        {
            return await _http.GetFromJsonAsync<ClienteResponseDto>($"api/clientes/{id}");
        }
    }
}
