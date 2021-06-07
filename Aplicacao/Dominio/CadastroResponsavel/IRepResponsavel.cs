using System.Linq;

namespace Aplicacao.Dominio.CadastroResponsavel
{
    public interface IRepResponsavel
    {
        IQueryable<Responsavel> Recuperar();

        Responsavel Find(int id);

        void Remover(Responsavel resp);

        void Attach(Responsavel responsavel);

        void SaveChanges();
    }
}
