using Aplicacao.Dominio;
using Aplicacao.Dominio.CadastroProcesso;
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
