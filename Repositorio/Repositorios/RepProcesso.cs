using Aplicacao.Dominio.Responsavel;
using System;
using System.Linq;

namespace Repositorio.Repositorios
{
    public class RepProcesso : IRepProcesso
    {
        public Processo Find(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Processo> Recuperar()
        {
            throw new NotImplementedException();
        }

        public void Salvar(Processo processo)
        {
            throw new NotImplementedException();
        }
    }
}
