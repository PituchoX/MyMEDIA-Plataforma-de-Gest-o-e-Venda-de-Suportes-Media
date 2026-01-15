using GestaoLoja.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestaoLoja.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<Subcategoria> Subcategorias { get; set; }
        public DbSet<Produtos> Produtos { get; set; }
        public DbSet<ModoEntrega> ModosEntrega { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Vendas> Vendas { get; set; }
        public DbSet<LinhasVenda> LinhasVenda { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

  
            builder.Entity<Produtos>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Produtos)
                .HasForeignKey(p => p.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Subcategoria>()
                .HasOne(s => s.Categoria)
                .WithMany(c => c.Subcategorias)
                .HasForeignKey(s => s.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Produtos>()
                .HasOne(p => p.Subcategoria)
                .WithMany(s => s.Produtos)
                .HasForeignKey(p => p.SubcategoriaId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Produtos>()
                .HasOne(p => p.Fornecedor)
                .WithMany(f => f.Produtos)
                .HasForeignKey(p => p.FornecedorId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Produtos>()
                .HasOne(p => p.ModoEntrega)
                .WithMany(m => m.Produtos)
                .HasForeignKey(p => p.ModoEntregaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
