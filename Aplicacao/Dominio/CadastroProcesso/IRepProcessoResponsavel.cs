using System.Linq;

namespace Aplicacao.Dominio.CadastroProcesso
{
    public interface IRepProcessoResponsavel
    {
        IQueryable<ProcessoResponsavel> Recuperar();

        void Salvar(ProcessoResponsavel ProcessoResponsavel);
    }
}
