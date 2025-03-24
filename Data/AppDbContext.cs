using Microsoft.EntityFrameworkCore;

namespace BrinquedosAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Tabelas existentes
        public DbSet<Brinquedo> Brinquedos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Estoque> Estoques { get; set; }

        // Novas tabelas
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<BrinquedoFornecedor> BrinquedoFornecedores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da relação entre Brinquedo e Categoria (1:N)
            modelBuilder.Entity<Brinquedo>()
                .HasOne(b => b.Categoria)
                .WithMany(c => c.Brinquedos)
                .HasForeignKey(b => b.Id_categoria)
                .OnDelete(DeleteBehavior.SetNull); // Não excluir brinquedos ao excluir uma categoria

            // Configuração da relação entre Brinquedo e Estoque (1:1)
            modelBuilder.Entity<Brinquedo>()
                .HasOne(b => b.Estoque)
                .WithOne(e => e.Brinquedo)
                .HasForeignKey<Estoque>(e => e.Id_brinquedo)
                .OnDelete(DeleteBehavior.Cascade); // Excluir estoque ao excluir um brinquedo

            // Configuração da relação muitos-para-muitos entre Brinquedo e Fornecedor
            modelBuilder.Entity<BrinquedoFornecedor>()
                .HasKey(bf => new { bf.BrinquedoId, bf.FornecedorId }); // Chave composta

            modelBuilder.Entity<BrinquedoFornecedor>()
                .HasOne(bf => bf.Brinquedo)
                .WithMany(b => b.BrinquedoFornecedores)
                .HasForeignKey(bf => bf.BrinquedoId)
                .OnDelete(DeleteBehavior.Cascade); // Excluir relações ao excluir brinquedo

            modelBuilder.Entity<BrinquedoFornecedor>()
                .HasOne(bf => bf.Fornecedor)
                .WithMany(f => f.BrinquedoFornecedores)
                .HasForeignKey(bf => bf.FornecedorId)
                .OnDelete(DeleteBehavior.Cascade); // Excluir relações ao excluir fornecedor

            // Configurações adicionais de entidades (se necessário)
            modelBuilder.Entity<Fornecedor>()
                .HasIndex(f => f.CNPJ)
                .IsUnique(); // Garante que CNPJ seja único

            modelBuilder.Entity<Brinquedo>()
                .Property(b => b.Preco)
                .HasColumnType("decimal(10,2)");
        }
    }
}