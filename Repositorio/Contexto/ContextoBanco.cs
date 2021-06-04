using Microsoft.EntityFrameworkCore;
using Repositorio.Mapeamento;

namespace Repositorio.Contexto
{
    public class ContextoBanco : DbContext
    {
        public ContextoBanco(DbContextOptions<ContextoBanco> options)
            : base(options)
        {

        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ResponsavelConfig());
            modelBuilder.ApplyConfiguration(new ProcessoConfig());
            modelBuilder.ApplyConfiguration(new ProcessoResponsavelConfig());

            base.OnModelCreating(modelBuilder);

        }
    }
}
