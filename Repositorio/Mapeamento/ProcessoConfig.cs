using Aplicacao.Dominio.CadastroProcesso;
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
            builder.Property(p => p.DataDistribuicao).HasColumnName("datadistribuicao");
            builder.Property(p => p.ProcessoSegredo).IsRequired().HasColumnName("processosegredo");
            builder.Property(p => p.PastaFisica).HasColumnName("pastafisica");
            builder.Property(p => p.Descricao).HasColumnName("descricao");
            builder.Property(p => p.Situacao).IsRequired().HasColumnName("situacao");
            builder.Property(p => p.CodigoProcessoPai).HasColumnName("idprocessopai");


            builder.OwnsOne(p => p.NumeroProcesso).Property(p => p.Value).IsRequired().HasColumnName("numeroprocesso");

            builder.HasOne(p => p.ProcessoPai)
                .WithMany(p=>p.ProcessoFilho)
                .HasForeignKey(p => p.CodigoProcessoPai);

            builder.HasMany(p => p.ProcessoResponsavel)
                .WithOne()
                .HasForeignKey(p => p.CodigoProcesso);

            //builder.HasMany(p => p.ProcessoFilho)
            //    .WithOne();
        }
    }
}
