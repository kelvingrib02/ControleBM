namespace ControleBM.Domain.Entities
{
    public class Cliente : Entity
    {
        public required string Nome { get; set; }
        public string? Telefone { get; set; }
        public decimal SaldoDevedor { get; private set; }
        public void AdicionarDivida(decimal valor)
        {
            SaldoDevedor += valor;
        }
        public void PagarDivida(decimal valor)
        {
            SaldoDevedor -= valor;
        }
    }
}