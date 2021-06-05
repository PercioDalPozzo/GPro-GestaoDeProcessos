using System.Linq;

namespace Aplicacao.Dominio
{
    public interface IRepProcessoResponsavel
    {
        IQueryable<ProcessoResponsavel> Recuperar();

        void Salvar(ProcessoResponsavel ProcessoResponsavel);
    }
}
