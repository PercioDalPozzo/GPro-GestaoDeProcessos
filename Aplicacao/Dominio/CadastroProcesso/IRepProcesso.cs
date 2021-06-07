using System.Linq;

namespace Aplicacao.Dominio.CadastroProcesso
{
    public interface IRepProcesso
    {
        IQueryable<Processo> Recuperar();

        Processo Find(int id);

        void Remover(Processo processo);

        void Attach(Processo processo);

        void SaveChanges();
    }
}
