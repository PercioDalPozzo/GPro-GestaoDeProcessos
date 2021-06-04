using Aplicacao.Dominio.Responsavel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositorio.Mapeamento
{
    public class ProcessoConfig : IEntityTypeConfiguration<Processo>
    {
        public void Configure(EntityTypeBuilder<Processo> builder)
        {
            builder.ToTable("processo");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).IsRequired().HasColumnName("id");
            builder.Property(p => p.NumeroProcesso).IsRequired().HasColumnName("numeroprocesso");
            builder.Property(p => p.DataDistribuicao).HasColumnName("datadistribuicao");


            builder.HasMany(p => p.ProcessoResponsavel)
                .WithOne()
                .HasForeignKey(p => p.CodigoProcesso);
        }
    }
}
