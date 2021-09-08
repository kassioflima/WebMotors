using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using WebMotors.Data.Mapping;
using WebMotors.Domain.Anuncios.Entities;

namespace WebMotors.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Anuncio> Contato { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AnuncioConfig).Assembly);
            modelBuilder.Ignore<Notification>();
        }
    }
}
