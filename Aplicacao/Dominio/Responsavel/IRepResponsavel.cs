using System.Linq;

namespace Aplicacao.Dominio
{
    public interface IRepResponsavel
    {
        IQueryable<Responsavel> Recuperar();

        Responsavel Find(int id);

        void Remover(Responsavel resp);

        void Salvar(Responsavel responsavel);

        void SaveChanges();
    }
}
