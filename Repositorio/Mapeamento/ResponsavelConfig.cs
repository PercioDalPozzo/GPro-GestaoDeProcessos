using Aplicacao.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositorio.Mapeamento
{
    public class ResponsavelConfig : IEntityTypeConfiguration<Responsavel>
    {
        public void Configure(EntityTypeBuilder<Responsavel> builder)
        {
            builder.ToTable("responsavel");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).IsRequired().HasColumnName("id");
            builder.Property(p => p.Nome).IsRequired().HasColumnName("nome");
            builder.Property(p => p.Foto).HasColumnName("foto");


            builder.OwnsOne(p => p.Cpf).Property(p => p.Value).IsRequired().HasColumnName("cpf");
            builder.OwnsOne(p => p.Email).Property(p => p.Value).IsRequired().HasColumnName("email");
        }
    }
}
