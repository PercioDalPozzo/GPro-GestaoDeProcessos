using Aplicacao.Dominio.Responsavel;
using Repositorio.Contexto;
using System;
using System.Linq;

namespace Repositorio.Repositorios
{
    public class RepProcessoResponsavel : IRepProcessoResponsavel
    {
        private readonly ContextoBanco _contexto;

        public RepProcessoResponsavel(ContextoBanco contexto)
        {
            _contexto = contexto;
        }

        public IQueryable<ProcessoResponsavel> Recuperar()
        {
            return _contexto.Set<ProcessoResponsavel>();
        }

        public void Salvar(ProcessoResponsavel ProcessoResponsavel)
        {
            throw new NotImplementedException();
        }
    }
}
