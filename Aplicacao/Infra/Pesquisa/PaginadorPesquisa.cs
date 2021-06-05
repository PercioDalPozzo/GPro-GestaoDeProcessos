using System;
using System.Linq;
using System.Linq.Expressions;

namespace Aplicacao.Infra
{
    public class PaginadorPesquisa<T>
    {
        private readonly IQueryable<T> _query;

        public PaginadorPesquisa(IQueryable<T> query)
        {
            _query = query;
        }

        public IQueryable<T> Paginar(FiltroPaginadoView filtro, Expression<Func<T, object>> ordenacaoPadrao)
        {
            return _query
                .OrderBy(ordenacaoPadrao)
                .Skip((filtro.Pagina - 1) * filtro.Limite)
                .Take(filtro.Limite);
        }

        public int QuantRegistros()
        {
            return _query.Count();
        }
    }
}
