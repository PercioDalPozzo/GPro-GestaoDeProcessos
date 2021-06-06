using System.Linq;

namespace Aplicacao.Dominio.CadastroProcesso
{
    public interface IRepProcesso
    {
        Processo Find(int id);

        IQueryable<Processo> Recuperar();

        void Salvar(Processo processo);
    }
}
