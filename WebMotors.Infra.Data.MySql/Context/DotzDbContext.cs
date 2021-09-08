using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dotz.Infra.Data.MySql.Context
{
    public class DotzDbContext : DbContext
    {
        public DotzDbContext(DbContextOptions<DotzDbContext> options) : base(options) { }

        //public DbSet<Endereco> Endereco { get; set; }
        //public DbSet<Pessoa> Pessoa { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("dbo");
            //modelBuilder.ApplyConfiguration(new EnderecoConfig());
            //modelBuilder.ApplyConfiguration(new ModeloCategoriaConfig());
            modelBuilder.Ignore<Notification>();
        }
    }
}
