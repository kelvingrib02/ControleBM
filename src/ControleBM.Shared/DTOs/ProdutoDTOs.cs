using ControleBM.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace ControleBM.Shared.DTOs
{
    public class ProdutoResponseDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal PrecoVenda { get; set; }
        public decimal CustoUnitario { get; set; }
        public int EstoqueAtual { get; set; }
        public TipoProduto Tipo { get; set; }
    }

    public class ProdutoRequestDto
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "O preço de venda deve ser maior que zero")]
        public decimal PrecoVenda { get; set; }
        public decimal CustoUnitario { get; set; }
        public int EstoqueAtual { get; set; }
        public TipoProduto Tipo { get; set; }
    }
}