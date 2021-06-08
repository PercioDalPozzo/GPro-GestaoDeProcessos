using Aplicacao.Dominio.CadastroProcesso;
using Aplicacao.Dominio.CadastroResponsavel;
using Aplicacao.Infra;
using System;
using System.Linq;

namespace Aplicacao.Aplicacao.CadastroProcesso
{
    public class AplicProcesso : IAplicProcesso
    {
        private readonly IRepProcesso _repProcesso;
        private readonly IRepResponsavel _repResponsavel;
        private readonly IRepProcessoResponsavel _repProcessoResponsavel;
        private readonly IValidadorProcesso _validadorProcesso;

        public AplicProcesso(IRepProcesso repProcesso, IRepResponsavel repResponsavel, IRepProcessoResponsavel repProcessoResponsavel, IValidadorProcesso validadorProcesso)
        {
            _repProcesso = repProcesso;
            _repResponsavel = repResponsavel;
            _repProcessoResponsavel = repProcessoResponsavel;
            _validadorProcesso = validadorProcesso;
        }

        public RetornoPrepararEdicaoView PrepararEdicao(IdView view)
        {
            var processo = _repProcesso.Find(view.Id);

            var retorno = new RetornoPrepararEdicaoView(processo);
            retorno.ProcessoResponsavel = processo.ProcessoResponsavel.Select(p => new RetornoPrepararEdicaoResponsavelView(p)).ToList();

            // Pai
            if (processo.ProcessoPai != null)
                retorno.ProcessoPai = new RetornoPrepararEdicaoView(processo.ProcessoPai);

            AdicionarFilhosRecursivo(processo, retorno);

            return retorno;
        }

        private void AdicionarFilhosRecursivo(Processo processo, RetornoPrepararEdicaoView retornoView)
        {
            foreach (var filho in processo.ProcessoFilho)
            {
                var novoFilhoView = new RetornoPrepararEdicaoView(filho);
                retornoView.ProcessoFilho.Add(novoFilhoView);

                AdicionarFilhosRecursivo(filho, novoFilhoView);
            }
        }



        public RetornoPesquisaView Pesquisar(FiltroPesquisarProcessoView filtro)
        {
            var query = AplicarFiltros(filtro);

            var paginador = new PaginadorPesquisa<Processo>(query);

            var registrosPaginados = paginador.Paginar(filtro, p => p.DataDistribuicao)
                .Select(p => new
                {
                    Id = p.Id,
                    NumeroProcesso = p.NumeroProcesso.Value,
                    DataDistribuicao = p.DataDistribuicao,
                    ProcessoSegredo = p.ProcessoSegredo,
                    PastaFisica = p.PastaFisica,
                    Descricao = p.Descricao,
                    Situacao = p.Situacao,
                    Responsaveis = p.ProcessoResponsavel.Select(r => r.Responsavel.Nome).ToList()
                })
                .ToList();

            var registros = registrosPaginados.Select(p => new RetornoPesquisarView
            {
                Id = p.Id,
                NumeroProcesso = NumeroProcesso.Formatar(p.NumeroProcesso),
                DataDistribuicao = p.DataDistribuicao,
                ProcessoSegredo = p.ProcessoSegredo,
                PastaFisica = p.PastaFisica,
                Descricao = p.Descricao,
                Situacao = p.Situacao,
                Responsaveis = string.Join(" - ", p.Responsaveis)
            }).ToList();


            var numerosDaConsulta = paginador.NumerosDaConsulta();

            return new RetornoPesquisaView
            {
                TotalPaginas = numerosDaConsulta.TotalPaginas,
                TotalRegistros = numerosDaConsulta.TotalRegistros,
                Registros = registros
            };
        }

        private IQueryable<Processo> AplicarFiltros(FiltroPesquisarProcessoView filtro)
        {
            var query = _repProcesso.Recuperar();

            if (!string.IsNullOrEmpty(filtro.NumeroProcesso))
            {
                var numeroSemFormato = NumeroProcesso.RemoverFormatacao(filtro.NumeroProcesso);
                query = query.Where(p => p.NumeroProcesso.Value == numeroSemFormato);
            }

            if (filtro.DataDistribuicaoIni.HasValue)
                query = query.Where(p => p.DataDistribuicao >= filtro.DataDistribuicaoIni);

            if (filtro.DataDistribuicaoFim.HasValue)
                query = query.Where(p => p.DataDistribuicao <= filtro.DataDistribuicaoFim);

            if (filtro.ProcessoSegredo)
                query = query.Where(p => p.ProcessoSegredo);

            if (!string.IsNullOrEmpty(filtro.PastaFisica))
                query = query.Where(p => p.PastaFisica.ToLower().Contains(filtro.PastaFisica.ToLower()));

            if (filtro.Situacao.HasValue)
                query = query.Where(p => p.Situacao == filtro.Situacao.Value);

            if (!string.IsNullOrEmpty(filtro.NomeResponsavel))
            {
                //query = query.Where(p => p.ProcessoResponsavel.Any(r => r.Responsavel.Nome.ToLower().Contains(filtro.NomeResponsavel.ToLower())));
                var codigos = _repResponsavel.Recuperar().Where(p => p.Nome.ToLower().Contains(filtro.NomeResponsavel.ToLower())).Select(p => p.Id).ToList();
                query = query.Where(p => p.ProcessoResponsavel.Any(r => codigos.Contains(r.CodigoResponsavel)));
            }

            return query;
        }

        public RetornoSalvarView Salvar(SalvarProcessoView view)
        {
            var processo = ResolverInserirEditar(view);

            processo.NumeroProcesso = new NumeroProcesso(view.NumeroProcesso);
            processo.DataDistribuicao = view.DataDistribuicao;
            processo.ProcessoSegredo = view.ProcessoSegredo;
            processo.PastaFisica = view.PastaFisica;
            processo.Descricao = view.Descricao;
            processo.Situacao = view.Situacao;
            if (view.CodigoProcessoPai.HasValue)
            {
                processo.SetarProcessoPai(_repProcesso.Find(view.CodigoProcessoPai.Value));
            }

            ResolveCrudResponsaveis(processo, view);

            _validadorProcesso.ValidarCadastro(processo);


            _repProcesso.SaveChanges();

            return new RetornoSalvarView(processo);
        }

        private void ResolveCrudResponsaveis(Processo processo, SalvarProcessoView view)
        {
            var codigosView = view.ProcessoResponsavel.Select(p => p.Id).ToList();

            // excluir
            var paraExcluir = processo.ProcessoResponsavel.Where(p => !codigosView.Contains(p.Id)).ToList();
            foreach (var reg in paraExcluir)
            {
                _repProcessoResponsavel.Remover(reg);
                processo.ProcessoResponsavel.Remove(reg);
            }

            // inserir
            var paraInserir = view.ProcessoResponsavel.Where(p => p.Inserindo()).ToList();
            foreach (var reg in paraInserir)
            {
                var responsavel = _repResponsavel.Find(reg.CodigoResponsavel);
                var novo = new ProcessoResponsavel(processo, responsavel);
                _repProcessoResponsavel.Attach(novo);
            }
        }

        private Processo ResolverInserirEditar(SalvarProcessoView view)
        {
            if (view.Inserindo())
            {
                var novo = new Processo();
                _repProcesso.Attach(novo);
                return novo;
            }

            var processo = _repProcesso.Find(view.Id);
            if (processo.Finalizado())
                throw new Exception("O processo está finalizado e não poderá ser editado.");
            return processo;
        }

        public void Remover(IdView view)
        {
            var processo = _repProcesso.Find(view.Id);

            _validadorProcesso.ValidarExclusao(processo);


            foreach (var procResp in processo.ProcessoResponsavel)
            {
                _repProcessoResponsavel.Remover(procResp);
            }
            _repProcesso.Remover(processo);
            _repProcesso.SaveChanges();
        }
    }
}
