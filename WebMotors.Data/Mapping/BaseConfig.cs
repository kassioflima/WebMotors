using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMotors.Domain.Shared.Entities;

namespace WebMotors.Data.Mapping;

public class BaseConfig<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Entity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasIndex(x => x.Id)
            .IsClustered(true)
            .IsUnique(true);

        builder.Property(c => c.Id)
            .HasColumnType("int")
            .ValueGeneratedOnAdd();
    }
}
