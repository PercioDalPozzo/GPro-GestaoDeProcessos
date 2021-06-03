using Aplicacao.Dominio.Responsavel;
using System;
using System.Linq;

namespace Repositorio.Repositorios
{
    public class RepProcessoResponsavel : IRepProcessoResponsavel
    {
        public IQueryable<ProcessoResponsavel> Recuperar()
        {
            throw new NotImplementedException();
        }

        public void Salvar(ProcessoResponsavel ProcessoResponsavel)
        {
            throw new NotImplementedException();
        }
    }
}
