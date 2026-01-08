using ControleBM.Domain.Enums; 

namespace ControleBM.Domain.Entities
{
    public class Produto : Entity
    {
        public string Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal PrecoVenda { get; set; }
        public decimal CustoUnitario { get; set; }
        public int EstoqueAtual { get; set; }
        public TipoProduto Tipo { get; set; }
        public bool Ativo { get; set; } = true;
        public void DebitarEstoque(int quantidade)
        {
            if (EstoqueAtual < quantidade)
            {
                
            }
            EstoqueAtual -= quantidade;
        }
    }
}