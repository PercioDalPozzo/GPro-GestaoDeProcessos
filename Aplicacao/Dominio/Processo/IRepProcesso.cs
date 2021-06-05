using System.Linq;

namespace Aplicacao.Dominio
{
    public interface IRepProcesso
    {
        Processo Find(int id);

        IQueryable<Processo> Recuperar();

        void Salvar(Processo processo);
    }
}
