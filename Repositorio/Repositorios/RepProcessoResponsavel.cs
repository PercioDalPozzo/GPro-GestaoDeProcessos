using Aplicacao.Dominio;
using Repositorio.Contexto;

namespace Repositorio.Repositorios
{
    public class RepProcessoResponsavel : RepBase<ProcessoResponsavel>, IRepProcessoResponsavel
    {
        public RepProcessoResponsavel(ContextoBanco contexto)
            : base(contexto)
        {
        }
    }
}
