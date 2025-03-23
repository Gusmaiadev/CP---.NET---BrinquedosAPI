using Microsoft.EntityFrameworkCore;

namespace BrinquedosAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Definir as DbSet para as tabelas
        public DbSet<Brinquedo> Brinquedos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Estoque> Estoques { get; set; }

        // Configurar as relações usando Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar a relação entre Brinquedo e Categoria (1:N)
            modelBuilder.Entity<Brinquedo>()
                .HasOne(b => b.Categoria)
                .WithMany(c => c.Brinquedos)
                .HasForeignKey(b => b.Id_categoria)
                .OnDelete(DeleteBehavior.SetNull); // Não excluir brinquedos ao excluir uma categoria

            // Configurar a relação entre Brinquedo e Estoque (1:1)
            modelBuilder.Entity<Brinquedo>()
                .HasOne(b => b.Estoque)
                .WithOne(e => e.Brinquedo)
                .HasForeignKey<Estoque>(e => e.Id_brinquedo)
                .OnDelete(DeleteBehavior.Cascade); // Excluir estoque ao excluir um brinquedo
        }
    }
}