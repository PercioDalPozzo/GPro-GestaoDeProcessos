using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aplicacao.Dominio.Responsavel
{
    public interface IRepResponsavel
    {
        Responsavel Find(int id);

        IQueryable<Responsavel> Recuperar();

        void Salvar(Responsavel responsavel);
    }
}
