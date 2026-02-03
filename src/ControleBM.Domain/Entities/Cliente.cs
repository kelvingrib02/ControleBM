namespace ControleBM.Domain.Entities
{
    public class Cliente : Entity
    {
        public required string Nome { get; set; }
        public string? Telefone { get; set; }
        public decimal SaldoDevedor { get; set; }
        public bool Ativo { get; set; } = true;
    }
}