using ReservaHotel.Models;
using Microsoft.EntityFrameworkCore;

namespace ReservaHotel.Data
{
    public class BDContext : DbContext
    {
        public DbSet<Cliente>? Clientes { get; set; }
        public DbSet<Servico>? Servicos { get; set; }
        public DbSet<Pacote>? Pacotes { get; set; }
        public DbSet<Quarto> Quartos {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource=hotel.db;Cache=Shared");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pacote>()
                .HasMany(p => p.Servicos)
                .WithOne();

        }
    }
}
