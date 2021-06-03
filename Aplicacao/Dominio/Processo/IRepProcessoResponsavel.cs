using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aplicacao.Dominio.Responsavel
{
    public interface IRepProcessoResponsavel
    {
        IQueryable<ProcessoResponsavel> Recuperar();

        void Salvar(ProcessoResponsavel ProcessoResponsavel);
    }
}
