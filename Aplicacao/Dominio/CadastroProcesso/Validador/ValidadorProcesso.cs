using Aplicacao.Infra;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aplicacao.Dominio.CadastroProcesso
{
    public class ValidadorProcesso : IValidadorProcesso
    {
        private readonly IRepProcesso _repProcesso;
        private readonly IRepProcessoResponsavel _repProcessoResponsavel;

        public ValidadorProcesso(IRepProcesso repProcesso, IRepProcessoResponsavel repProcessoResponsavel)
        {
            _repProcesso = repProcesso;
            _repProcessoResponsavel = repProcessoResponsavel;
        }


        public void ValidarCadastro(Processo processo)
        {
            processo.ValidarNumeroProcesso();
            ExcecaoSeTiverDuplicidade(processo);

            processo.ValidarDataDistribuicao();
            processo.ValidarPastaFisica();
            processo.ValidarDescricao();

            processo.ValidarResponsaveis();
            ExcecaoSeDuplicadoNaHierarquia(processo);
            ExcecaoSeHierarquiaExcederAvoPaiFilhoNeto(processo);
        }

        private void ExcecaoSeDuplicadoNaHierarquia(Processo processo)
        {
            if (processo.CodigoProcessoPai == processo.Id)
                throw new Exception("O processo não pode estar vinculado a ele mesmo.");

            // Filho
            var processoFilho = _repProcesso.Recuperar().Where(p => p.CodigoProcessoPai == processo.Id).Select(p => new { p.Id }).FirstOrDefault();
            if (processoFilho != null)
            {
                if (processo.CodigoProcessoPai == processoFilho.Id)
                    throw new Exception("O processo vinculado já faz parte da herarquia de processos (Nível 1).");

                // Neto
                var processoNeto = _repProcesso.Recuperar().Where(p => p.CodigoProcessoPai == processoFilho.Id).Select(p => new { p.Id }).FirstOrDefault();
                if (processo.CodigoProcessoPai == processoNeto.Id)
                    throw new Exception("O processo vinculado já faz parte da herarquia de processos (Nível 2).");
            }
        }

        private void ExcecaoSeHierarquiaExcederAvoPaiFilhoNeto(Processo processo)
        {
            var todaCadeia = BuscarHierarquiaAntecessor(processo);
            todaCadeia.Add(processo.Id);
            todaCadeia.AddRange(BuscarHierarquiaSucessor(processo));

            if (todaCadeia.Count > 4)
                throw new Exception("A ligação com o processo vinculado excede o limite de 4 processos na hierarquia.");
        }

        private List<int> BuscarHierarquiaSucessor(Processo processo)
        {
            var retorno = new List<int>();

            var processoFilho = _repProcesso.Recuperar().Where(p => p.CodigoProcessoPai == processo.Id).Select(p => new { p.Id }).FirstOrDefault();
            while (processoFilho != null)
            {
                retorno.Add(processoFilho.Id);
                processoFilho = _repProcesso.Recuperar().Where(p => p.CodigoProcessoPai == processoFilho.Id).Select(p => new { p.Id }).FirstOrDefault();
            }

            return retorno;
        }

        private List<int> BuscarHierarquiaAntecessor(Processo processo)
        {
            var retorno = new List<int>();
            var processoDaVez = processo;

            while (processoDaVez.CodigoProcessoPai.HasValue)
            {
                retorno.Add(processoDaVez.CodigoProcessoPai.Value);
                processoDaVez = processoDaVez.ProcessoPai;
            }

            return retorno;
        }


        private void ExcecaoSeTiverDuplicidade(Processo processo)
        {
            var existente = _repProcesso.Recuperar()
                                           .Where(p => p.NumeroProcesso.Value == processo.NumeroProcesso.Value && p.Id != processo.Id)
                                           .Select(p => new { NumeroProcesso = p.NumeroProcesso.Value, p.PastaFisica, p.DataDistribuicao })
                                           .FirstOrDefault();
            if (existente != null)
                throw new Exception(string.Format("O número do processo unificado {0} pertence a outro processo. (Pasta física: {1} / Data Distribuição: {2}).",
                    NumeroProcesso.Formatar(existente.NumeroProcesso),
                    existente.PastaFisica,
                    existente.DataDistribuicao.HasValue ? existente.DataDistribuicao.Value.NossoFormato() : "Sem data"
                    ));
        }
    }
}
