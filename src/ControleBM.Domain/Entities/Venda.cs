namespace ControleBM.Domain.Entities
{
    public class Venda : Entity
    {
        public DateTime DataVenda { get; set; }
        public decimal Total { get; set; }
        public int? ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        public bool Fiado { get; set; }
        public List<ItemVenda> Itens { get; set; } = new();
        public void CalcularTotal()
        {
            Total = Itens.Sum(i => i.SubTotal);
        }
    }
}
