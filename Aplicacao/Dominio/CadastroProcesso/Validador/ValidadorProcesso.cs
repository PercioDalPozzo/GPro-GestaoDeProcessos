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
            ExcecaoSeHierarquiaExcederQuatroNiveis(processo);
        }

        private void ExcecaoSeDuplicadoNaHierarquia(Processo processo)
        {
            if (processo.CodigoProcessoPai == processo.Id)
                throw new Exception("O processo não pode estar vinculado a ele mesmo.");

            var codigosHierarquia = new List<int>() { };
            var processoDaVez = processo;
            while (processoDaVez != null && processoDaVez.CodigoProcessoPai.HasValue)
            {
                if (codigosHierarquia.Contains(processoDaVez.Id))
                    throw new Exception("O processo já faz parte da hierarquia.");

                codigosHierarquia.Add(processoDaVez.Id);
                processoDaVez = processoDaVez.ProcessoPai;
            }
        }

        private void ExcecaoSeHierarquiaExcederQuatroNiveis(Processo processo)
        {
            // Aqui cabe um comentario.
            // Primeiramente é obtido a quantidade de niveis dos antecessores            
            var quantNiveisAntecessores = BuscarQuantHierarquiaAntecessor(processo);


            BuscarMaiorHierarquiaSucessora(processo, quantNiveisAntecessores, 0);
        }

        private int BuscarQuantHierarquiaAntecessor(Processo processo)
        {
            int count = 1;
            var processoDaVez = processo;
            while (processoDaVez != null && processoDaVez.CodigoProcessoPai.HasValue)
            {
                count++;
                processoDaVez = processoDaVez.ProcessoPai;
            }
            return count;
        }

        private void BuscarMaiorHierarquiaSucessora(Processo processo, int quantNiveisAntecessores, int quantNiveisFilho)
        {
            if ((quantNiveisAntecessores + quantNiveisFilho) > 4)
                throw new Exception("A ligação com o processo vinculado irá excede o limite de 4 processos na hierarquia.");

            // Se o processo tiver filho, deve incrementar o contador
            // Caso contrário, contador do filho é zerado
            if (processo.ProcessoFilho.Any())
                quantNiveisFilho++;
            else
                quantNiveisFilho = 0;

            foreach (var filho in processo.ProcessoFilho)
            {
                BuscarMaiorHierarquiaSucessora(filho, quantNiveisAntecessores, quantNiveisFilho);
            }
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

        public void ValidarExclusao(Processo processo)
        {
            if (processo.Finalizado())
                throw new Exception("O processo está finalizado e não poderá ser removido.");

            if (processo.ProcessoFilho.Any())
                throw new Exception("O processo está vinculado a outro(s) processo(s) e não pode ser removido.");
        }
    }
}
