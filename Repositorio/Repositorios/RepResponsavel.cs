using Aplicacao.Dominio.Responsavel;
using System;
using System.Linq;

namespace Repositorio.Repositorios
{
    public class RepResponsavel : IRepResponsavel
    {
        public Responsavel Find(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Responsavel> Recuperar()
        {
            throw new NotImplementedException();
        }

        public void Salvar(Responsavel responsavel)
        {
            throw new NotImplementedException();
        }
    }
}
