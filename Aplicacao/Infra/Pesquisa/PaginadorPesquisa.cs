using Aplicacao.Infra.Pesquisa;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Aplicacao.Infra
{
    public class PaginadorPesquisa<T>
    {
        private readonly IQueryable<T> _query;
        private FiltroPaginadoView _filtro;

        public PaginadorPesquisa(IQueryable<T> query)
        {
            _query = query;
        }

        public IQueryable<T> Paginar(FiltroPaginadoView filtro, Expression<Func<T, object>> ordenacaoPadrao)
        {
            _filtro = filtro;
            if (_filtro.Limite <= 0)
                throw new Exception("Deve ser informado a quantidade por pagina da pesquisa.");

            if (_filtro.Limite > 100)
                throw new Exception("O limite para paginação é de 100 registros.");

            return _query
                .OrderBy(ordenacaoPadrao)
                .Skip((_filtro.Pagina - 1) * _filtro.Limite)
                .Take(_filtro.Limite);
        }

        internal RetornoNumerosPesquisaView NumerosDaConsulta()
        {
            if (_filtro == null)
                throw new Exception("Deve ser feita a paginação antes de obter os numeros da pesquisa.");

            var retorno = new RetornoNumerosPesquisaView();
            retorno.TotalRegistros = _query.Count();
            retorno.TotalPaginas = (int)Math.Truncate((decimal)retorno.TotalRegistros / _filtro.Limite) + 1;
            return retorno;
        }
    }
}
