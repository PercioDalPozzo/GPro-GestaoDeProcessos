using Aplicacao.Dominio.Responsavel;
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
            builder.Property(p => p.Cpf).IsRequired().HasColumnName("cpf");
            builder.Property(p => p.Email).IsRequired().HasColumnName("email");
        }
    }
}
