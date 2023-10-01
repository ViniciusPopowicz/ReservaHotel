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
        public DbSet<Premio>? Premios {get; set;}
        public DbSet<Pagamento>? Pagamentos {get; set;}
        public DbSet<Recibo>? Recibos {get; set;}


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=hotel.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Servico>().HasMany(s => s.Pacotes);
            modelBuilder.Entity<Pacote>().HasMany(p => p.Servicos);
            modelBuilder.Entity<Reserva>().HasOne(r => r.Quarto);
            modelBuilder.Entity<Reserva>().HasOne(r => r.Hotel);
            modelBuilder.Entity<Reserva>().HasOne(r => r.Pacote);
            modelBuilder.Entity<Reserva>().HasOne(r => r.Cliente);
            modelBuilder.Entity<Reserva>().HasOne(r => r.Voucher);
            modelBuilder.Entity<Pagamento>().HasOne(p => p.Reserva);
            modelBuilder.Entity<Recibo>().HasOne(rec => rec.Pagamento);
            modelBuilder.Entity<Recibo>().HasOne(rec => rec.Premio);
        }
    }
}
