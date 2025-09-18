using Flunt.Notifications;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebMotors.Data.Mapping;
using WebMotors.Domain.Anuncios.Entities;
using WebMotors.Domain.Shared.Models;

namespace WebMotors.Data.Context;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Anuncio> Contato { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Configurar Identity para usar varchar ao invés de nvarchar
        ConfigureIdentityForVarchar(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AnuncioConfig).Assembly);
        modelBuilder.Ignore<Notification>();
    }

    private void ConfigureIdentityForVarchar(ModelBuilder modelBuilder)
    {
        // Configurar ApplicationUser para usar varchar
        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.Property(e => e.Id).HasColumnType("varchar(450)");
            entity.Property(e => e.UserName).HasColumnType("varchar(256)");
            entity.Property(e => e.NormalizedUserName).HasColumnType("varchar(256)");
            entity.Property(e => e.Email).HasColumnType("varchar(256)");
            entity.Property(e => e.NormalizedEmail).HasColumnType("varchar(256)");
            entity.Property(e => e.PasswordHash).HasColumnType("varchar(max)");
            entity.Property(e => e.SecurityStamp).HasColumnType("varchar(max)");
            entity.Property(e => e.ConcurrencyStamp).HasColumnType("varchar(max)");
            entity.Property(e => e.PhoneNumber).HasColumnType("varchar(max)");
            entity.Property(e => e.FullName).HasColumnType("varchar(max)");
        });

        // Configurar IdentityRole para usar varchar
        modelBuilder.Entity<Microsoft.AspNetCore.Identity.IdentityRole>(entity =>
        {
            entity.Property(e => e.Id).HasColumnType("varchar(450)");
            entity.Property(e => e.Name).HasColumnType("varchar(256)");
            entity.Property(e => e.NormalizedName).HasColumnType("varchar(256)");
            entity.Property(e => e.ConcurrencyStamp).HasColumnType("varchar(max)");
        });

        // Configurar IdentityRoleClaim para usar varchar
        modelBuilder.Entity<Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>>(entity =>
        {
            entity.Property(e => e.RoleId).HasColumnType("varchar(450)");
            entity.Property(e => e.ClaimType).HasColumnType("varchar(max)");
            entity.Property(e => e.ClaimValue).HasColumnType("varchar(max)");
        });

        // Configurar IdentityUserClaim para usar varchar
        modelBuilder.Entity<Microsoft.AspNetCore.Identity.IdentityUserClaim<string>>(entity =>
        {
            entity.Property(e => e.UserId).HasColumnType("varchar(450)");
            entity.Property(e => e.ClaimType).HasColumnType("varchar(max)");
            entity.Property(e => e.ClaimValue).HasColumnType("varchar(max)");
        });

        // Configurar IdentityUserLogin para usar varchar
        modelBuilder.Entity<Microsoft.AspNetCore.Identity.IdentityUserLogin<string>>(entity =>
        {
            entity.Property(e => e.LoginProvider).HasColumnType("varchar(450)");
            entity.Property(e => e.ProviderKey).HasColumnType("varchar(450)");
            entity.Property(e => e.ProviderDisplayName).HasColumnType("varchar(max)");
            entity.Property(e => e.UserId).HasColumnType("varchar(450)");
        });

        // Configurar IdentityUserRole para usar varchar
        modelBuilder.Entity<Microsoft.AspNetCore.Identity.IdentityUserRole<string>>(entity =>
        {
            entity.Property(e => e.UserId).HasColumnType("varchar(450)");
            entity.Property(e => e.RoleId).HasColumnType("varchar(450)");
        });

        // Configurar IdentityUserToken para usar varchar
        modelBuilder.Entity<Microsoft.AspNetCore.Identity.IdentityUserToken<string>>(entity =>
        {
            entity.Property(e => e.UserId).HasColumnType("varchar(450)");
            entity.Property(e => e.LoginProvider).HasColumnType("varchar(450)");
            entity.Property(e => e.Name).HasColumnType("varchar(450)");
            entity.Property(e => e.Value).HasColumnType("varchar(max)");
        });
    }
}
