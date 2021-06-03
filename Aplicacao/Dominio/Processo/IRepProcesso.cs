using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aplicacao.Dominio.Responsavel
{
    public interface IRepProcesso
    {
        Processo Find(int id);

        IQueryable<Processo> Recuperar();

        void Salvar(Processo processo);
    }
}
