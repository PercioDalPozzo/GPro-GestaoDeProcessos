using Aplicacao.Dominio;
using Repositorio.Contexto;

namespace Repositorio.Repositorios
{
    public class RepProcesso : RepBase<Processo>, IRepProcesso
    {
        public RepProcesso(ContextoBanco contexto)
            : base(contexto)
        {

        }
    }
}
