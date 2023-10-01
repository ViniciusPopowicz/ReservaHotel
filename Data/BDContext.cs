using ReservaHotel.Models;
using Microsoft.EntityFrameworkCore;

namespace ReservaHotel.Data
{
    public class BDContext : DbContext
    {
        public DbSet<Cliente>? Clientes { get; set; }
        public DbSet<Servico>? Servicos { get; set; }
        public DbSet<Pacote>? Pacotes { get; set; }
        public DbSet<Quarto>? Quartos {get; set;}
        public DbSet<Reserva>? Reservas {get; set;}
        public DbSet<Voucher>? Vouchers {get; set;}
        public DbSet<Hotel>? Hotels {get; set;}


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=hotel.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Servico>().HasMany(p => p.Pacotes);
            modelBuilder.Entity<Pacote>().HasMany(p => p.Servicos);
            modelBuilder.Entity<Reserva>().HasOne(p => p.Quarto);
            modelBuilder.Entity<Reserva>().HasOne(p => p.Hotel);
            modelBuilder.Entity<Reserva>().HasOne(p => p.Pacote);
            modelBuilder.Entity<Reserva>().HasOne(p => p.Cliente);
            modelBuilder.Entity<Reserva>().HasOne(p => p.Voucher);
            

        }
    }
}
