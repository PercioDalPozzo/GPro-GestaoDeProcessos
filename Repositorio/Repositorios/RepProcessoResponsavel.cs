using Aplicacao.Dominio;
using Aplicacao.Dominio.CadastroProcesso;
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
