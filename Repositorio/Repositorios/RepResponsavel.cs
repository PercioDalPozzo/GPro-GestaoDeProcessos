using Aplicacao.Dominio.Responsavel;
using Repositorio.Contexto;
using System;
using System.Linq;

namespace Repositorio.Repositorios
{
    public class RepResponsavel : IRepResponsavel
    {
        private readonly ContextoBanco _contexto;

        public RepResponsavel(ContextoBanco contexto)
        {
            _contexto = contexto;
        }

        public Responsavel Find(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Responsavel> Recuperar()
        {
            return _contexto.Set<Responsavel>();
        }

        public void Salvar(Responsavel responsavel)
        {
            throw new NotImplementedException();
        }
    }
}
