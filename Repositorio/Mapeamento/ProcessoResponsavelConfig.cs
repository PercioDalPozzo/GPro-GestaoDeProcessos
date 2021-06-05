using Aplicacao.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositorio.Mapeamento
{
    public class ProcessoResponsavelConfig : IEntityTypeConfiguration<ProcessoResponsavel>
    {
        public void Configure(EntityTypeBuilder<ProcessoResponsavel> builder)
        {
            builder.ToTable("processoresponsavel");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).IsRequired().HasColumnName("id");
            builder.Property(p => p.CodigoProcesso).IsRequired().HasColumnName("idprocesso");
            builder.Property(p => p.CodigoResponsavel).IsRequired().HasColumnName("idresponsavel");

            builder.HasOne(p => p.Processo)
                .WithMany(p=>p.ProcessoResponsavel)
                .HasForeignKey(p => p.CodigoProcesso);

            
            builder.HasOne(p => p.Responsavel)
                .WithMany()
                .HasForeignKey(p => p.CodigoResponsavel);
        }
    }
}
