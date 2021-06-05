using Aplicacao.Dominio;
using Aplicacao.Infra;
using Aplicacao.Infra.ObjectValue;
using System.Collections.Generic;
using System.Linq;

namespace Aplicacao.Aplicacao
{
    public class AplicResponsavel : IAplicResponsavel
    {
        private readonly IRepResponsavel _repResponsavel;
        private readonly IRepProcessoResponsavel _repProcessoResponsavel;
        private readonly IValidadorResponsavel _validadorResponsavel;

        public AplicResponsavel(IRepResponsavel repResponsavel, IRepProcessoResponsavel repProcessoResponsavel, IValidadorResponsavel validadorResponsavel)
        {
            _repResponsavel = repResponsavel;
            _repProcessoResponsavel = repProcessoResponsavel;
              _validadorResponsavel = validadorResponsavel;
        }

        public RetornoPrepararEdicaoView PrepararEdicao(IdView view)
        {
            var responsavel = _repResponsavel.Find(view.Id);

            var retorno = new RetornoPrepararEdicaoView();
            retorno.Id = responsavel.Id;
            retorno.Nome = responsavel.Nome;
            retorno.Cpf = responsavel.Cpf.Value;
            retorno.Email = responsavel.Email.Value;
            retorno.Foto = responsavel.Foto;

            var todosProcessos = _repProcessoResponsavel.Recuperar()
                                                        .Where(p => p.CodigoResponsavel == view.Id)
                                                        .Select(p => new
                                                        {
                                                            NumeroProcesso = p.Processo.NumeroProcesso.Value,
                                                            p.Processo.DataDistribuicao,
                                                            p.Processo.ProcessoSegredo,
                                                            p.Processo.PastaFisica,
                                                            p.Processo.Descricao,
                                                            p.Processo.Situacao,
                                                            Responsaveis = p.Processo.ProcessoResponsavel.Select(r => r.Responsavel.Nome).ToList()
                                                        }).ToList();
            foreach (var processo in todosProcessos)
            {
                retorno.Processos.Add(new ProcessoView()
                {
                    NumeroProcesso = NumeroProcesso.Formatar(processo.NumeroProcesso),
                    DataDistribuicao = processo.DataDistribuicao,
                    ProcessoSegredo = processo.ProcessoSegredo,
                    PastaFisica = processo.PastaFisica,
                    Descricao = processo.Descricao,
                    Situacao = processo.Situacao,
                    Responsaveis = string.Join(" - ", processo.Responsaveis)
                });
            }
            return retorno;
        }

        public RetornoPesquisaView Pesquisar(FiltroPesquisarView filtro)
        {
            var query = AplicarFiltros(filtro);

            var paginador = new PaginadorPesquisa<Responsavel>(query);

            var registros = paginador.Paginar(filtro, p => p.Nome)
                .Select(p => new RetornoPesquisarView
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Cpf = p.Cpf.Value,
                })
                .ToList();

            CarregarProcessosParaListagem(registros);

            return new RetornoPesquisaView
            {
                TotalRegistros = paginador.QuantRegistros(),
                Registros = registros
            };
        }

        private IQueryable<Responsavel> AplicarFiltros(FiltroPesquisarView filtro)
        {
            var query = _repResponsavel.Recuperar();

            if (!string.IsNullOrEmpty(filtro.Nome))
                query = query.Where(p => p.Nome.ToLower().Contains(filtro.Nome.ToLower()));

            if (!string.IsNullOrEmpty(filtro.Cpf))
            {
                var cfpSomenteNumeros = filtro.Cpf.SomenteNumeros();
                query = query.Where(p => p.Cpf.Value.Contains(cfpSomenteNumeros));
            }

            if (!string.IsNullOrEmpty(filtro.NumeroProcesso))
            {
                var numeroSemFormato = NumeroProcesso.RemoverFormatacao(filtro.NumeroProcesso);
                var codigosResponsaveis = _repProcessoResponsavel.Recuperar()
                                                                 .Where(p => p.Processo.NumeroProcesso.Value.ToLower() == numeroSemFormato.ToLower())
                                                                 .Select(p => p.CodigoResponsavel)
                                                                 .ToList();
                query = query.Where(p => codigosResponsaveis.Contains(p.Id));
            }

            return query;
        }

        private void CarregarProcessosParaListagem(List<RetornoPesquisarView> retorno)
        {
            var codigosResponsaveis = retorno.Select(p => p.Id).ToList();
            var todosProcessos = _repProcessoResponsavel.Recuperar()
                                                   .Where(p => codigosResponsaveis.Contains(p.CodigoResponsavel))
                                                   .Select(p => new { p.CodigoProcesso, NumeroProcesso = p.Processo.NumeroProcesso.Value, p.Processo.DataDistribuicao, p.CodigoResponsavel })
                                                   .ToList();
            foreach (var ret in retorno)
            {
                var processos = todosProcessos.Where(p => p.CodigoResponsavel == ret.Id).OrderBy(p => p.DataDistribuicao).Select(p => NumeroProcesso.Formatar(p.NumeroProcesso)).ToList();
                if (processos.Any())
                {
                    var concat = string.Join(" - ", processos);
                    ret.Processos = concat;
                }
            }
        }

        public RetornoSalvarView Salvar(SalvarResponsavelView view)
        {
            var resp = ResolverInserirEditar(view);
            resp.Nome = view.Nome;
            resp.Email = new Email(view.Email);
            resp.Cpf = new Cpf(view.Cpf.SomenteNumeros());

            if (view.AtualizarFoto)
                resp.Foto = view.Foto;

            _validadorResponsavel.ValidarCadastro(resp);

            _repResponsavel.Salvar(resp);
            _repResponsavel.SaveChanges();

            return new RetornoSalvarView(resp);
        }

        private Responsavel ResolverInserirEditar(SalvarResponsavelView view)
        {
            if (view.Inserindo())
                return new Responsavel();

            return _repResponsavel.Find(view.Id);

        }

        public void Remover(IdView view)
        {
            var resp = _repResponsavel.Find(view.Id);

            _validadorResponsavel.ExcecaoSeTiverVinculo(resp);

            _repResponsavel.Remover(resp);
            _repResponsavel.SaveChanges();
        }
    }
}
