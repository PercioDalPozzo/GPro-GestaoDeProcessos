using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aplicacao.Dominio
{
    public interface IRepResponsavel
    {
        Responsavel Find(int id);

        IQueryable<Responsavel> Recuperar();

        void Salvar(Responsavel responsavel);

        void SaveChanges();
    }
}
