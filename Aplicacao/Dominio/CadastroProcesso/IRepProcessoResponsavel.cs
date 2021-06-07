using System.Linq;

namespace Aplicacao.Dominio.CadastroProcesso
{
    public interface IRepProcessoResponsavel
    {
        IQueryable<ProcessoResponsavel> Recuperar();

        void Remover(ProcessoResponsavel processoResponsavel);

        void Salvar(ProcessoResponsavel ProcessoResponsavel);
    }
}
