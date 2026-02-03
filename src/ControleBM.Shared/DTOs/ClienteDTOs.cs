using System.ComponentModel.DataAnnotations;

namespace ControleBM.Shared.DTOs
{
    public class ClienteResponseDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public decimal SaldoDevedor { get; set; }
    }

    public class ClienteRequestDto
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public decimal SaldoDevedor { get; set; }
    }
}