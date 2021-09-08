using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMotors.Domain.Anuncios.Entities;

namespace WebMotors.Data.Mapping
{
    public class AnuncioConfig : IEntityTypeConfiguration<Anuncio>
    {
        public void Configure(EntityTypeBuilder<Anuncio> builder)
        {
            builder.ToTable("teste_webmotors");

            builder.Property(c => c.Marca).HasColumnType("varchar(45)").IsUnicode(false);
            builder.Property(c => c.Modelo).HasColumnType("varchar(45)").IsUnicode(false);
            builder.Property(c => c.Versao).HasColumnType("varchar(45)").IsUnicode(false);
            builder.Property(c => c.Ano).HasColumnType("int");
            builder.Property(c => c.Quilometragem).HasColumnType("int");
            builder.Property(c => c.Observacao).HasColumnType("varchar(512)").IsUnicode(false);
        }
    }
}
