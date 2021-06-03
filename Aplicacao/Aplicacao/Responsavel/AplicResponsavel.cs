using Aplicacao.Dominio.Responsavel;
using System.Collections.Generic;
using System.Linq;

namespace Aplicacao.Aplicacao.Responsavel
{
    public class AplicResponsavel : IAplicResponsavel
    {
        private readonly IRepResponsavel _repResponsavel;
        private readonly IRepProcessoResponsavel _repProcessoResponsavel;

        public AplicResponsavel(IRepResponsavel repResponsavel, IRepProcessoResponsavel repProcessoResponsavel)
        {
            _repResponsavel = repResponsavel;
            _repProcessoResponsavel = repProcessoResponsavel;
        }

        public List<RetornoPesquisarView> Pesquisar(FiltroPesquisarView filtro)
        {
            var query = _repResponsavel.Recuperar();

            if (!string.IsNullOrEmpty(filtro.Nome))
                query = query.Where(p => filtro.Nome.Contains(p.Nome));

            if (!string.IsNullOrEmpty(filtro.Cpf))
                query = query.Where(p => filtro.Cpf.Contains(p.Cpf));

            if (!string.IsNullOrEmpty(filtro.NumeroProcesso))
            {
                var codigosResponsaveis = _repProcessoResponsavel.Recuperar()
                                                                 .Where(p => p.Processo.NumeroProcesso == filtro.NumeroProcesso)
                                                                 .Select(p => p.CodigoResponsavel)
                                                                 .ToList();
                query = query.Where(p => codigosResponsaveis.Contains(p.Id));
            }

            var retorno = query
                .Take(filtro.Limite)
                .Skip(filtro.Pagina)
                .Select(p => new RetornoPesquisarView
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Cpf = p.Cpf,
                })
                .OrderBy(p => p.Nome)
                .ToList();

            CarregarProcessosParaListagem(retorno);

            return retorno;
        }

        private void CarregarProcessosParaListagem(List<RetornoPesquisarView> retorno)
        {
            var codigosResponsaveis = retorno.Select(p => p.Id).ToList();
            var todosProcessos = _repProcessoResponsavel.Recuperar()
                                                   .Where(p => codigosResponsaveis.Contains(p.CodigoResponsavel))
                                                   .Select(p => new { p.CodigoProcesso, p.Processo.NumeroProcesso, p.Processo.DataDistribuicao, p.CodigoResponsavel })
                                                   .ToList();

            foreach (var ret in retorno)
            {
                var processos = todosProcessos.Where(p => p.CodigoResponsavel == ret.Id).OrderBy(p => p.DataDistribuicao).Select(p => p.NumeroProcesso).ToList();
                if (processos.Any())
                {
                    var concat = string.Join("/", processos);
                    ret.Processos = concat;
                }
            }
        }
    }
}
