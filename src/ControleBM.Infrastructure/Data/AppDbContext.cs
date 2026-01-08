using ControleBM.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControleBM.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<ItemVenda> ItensVenda { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ItemVenda>().Property(i => i.PrecoUnitarioMomento).HasPrecision(18, 2);

            modelBuilder.Entity<ItemVenda>().Property(i => i.CustoUnitarioMomento).HasPrecision(18, 2);

            modelBuilder.Entity<Produto>().Property(p => p.PrecoVenda).HasPrecision(18, 2);

            modelBuilder.Entity<Produto>().Property(p => p.PrecoCusto).HasPrecision(18, 2);
        }
    }
}