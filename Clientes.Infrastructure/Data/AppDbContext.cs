using Clientes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clientes.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        // DbSets sempre inicializados com null! para evitar warning de nullable
        public DbSet<Cliente> Clientes { get; set; } = null!;
        public DbSet<Telefone> Telefones { get; set; } = null!;
        public DbSet<TipoTelefone> TiposTelefone { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Chave primária do Cliente
            modelBuilder.Entity<Cliente>()
                .HasKey(c => c.CodigoCliente);

            // Relacionamento Cliente -> Telefones
            modelBuilder.Entity<Telefone>()
                .HasOne(t => t.Cliente)
                .WithMany(c => c.Telefones)
                .HasForeignKey(t => t.CodigoCliente);

            // Relacionamento Telefone -> TipoTelefone
            modelBuilder.Entity<Telefone>()
                .HasOne(t => t.TipoTelefone)
                .WithMany(tp => tp.Telefones)
                .HasForeignKey(t => t.CodigoTipoTelefone);
        }
    }
}
