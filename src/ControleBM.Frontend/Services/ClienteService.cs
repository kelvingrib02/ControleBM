using System.Net.Http.Json;
using ControleBM.Shared.DTOs;

namespace ControleBM.Frontend.Services
{
    public interface IClienteService
    {
        Task<List<ClienteResponseDto>> GetClienteAsync();
        Task<ClienteResponseDto?> GetClienteByIdAsync(Guid id);
        Task CreateClienteAsync(ClienteRequestDto cliente);
        Task UpdateClienteAsync(Guid id, ClienteRequestDto cliente);
        Task DeleteClienteAsync(Guid id);
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

        public async Task CreateClienteAsync(ClienteRequestDto cliente)
        {
            var response = await _http.PostAsJsonAsync("api/clientes", cliente);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateClienteAsync(Guid id, ClienteRequestDto cliente)
        {
            var response = await _http.PutAsJsonAsync($"api/clientes/{id}", cliente);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteClienteAsync(Guid id)
        {
            var response = await _http.DeleteAsync($"api/clientes/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
