using Aplicacao.Dominio;
using Aplicacao.Dominio.CadastroResponsavel;
using Repositorio.Contexto;

namespace Repositorio.Repositorios
{
    public class RepResponsavel : RepBase<Responsavel>, IRepResponsavel
    {
        public RepResponsavel(ContextoBanco contexto)
            : base(contexto)
        {
        }
    }
}
