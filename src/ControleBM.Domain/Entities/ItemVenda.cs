namespace ControleBM.Domain.Entities
{
    public class ItemVenda : Entity
    {
        public Guid VendaId { get; set; }
        public Venda Venda { get; set; } = default!;
        public Guid ProdutoId { get; set; }
        public Produto Produto { get; set; } = default!;
        public int Quantidade { get; set; }
        public decimal PrecoUnitarioCobrado { get; set; }
        public decimal CustoUnitarioNoMomento { get; set; }
        public decimal SubTotal => Quantidade * PrecoUnitarioCobrado;
        public decimal LucroEstimado => (PrecoUnitarioCobrado - CustoUnitarioNoMomento) * Quantidade;
    }
}