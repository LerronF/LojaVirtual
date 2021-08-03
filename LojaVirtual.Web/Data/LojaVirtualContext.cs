using LojaVirtual.Web.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace LojaVirtual.Web.Data
{
    public class LojaVirtualContext : DbContext
    {
        public LojaVirtualContext() : base("LojaVirtual")
        {
        }

        public DbSet<Vendas> Vendas { get; set; }
        public DbSet<VendasItens> VendasItens { get; set; }
        public DbSet<Produtos> Produto { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}