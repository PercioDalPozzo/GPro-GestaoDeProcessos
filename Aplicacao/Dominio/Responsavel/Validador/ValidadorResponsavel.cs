using Aplicacao.Infra.ObjectValue;
using System;
using System.Linq;

namespace Aplicacao.Dominio
{
    public class ValidadorResponsavel : IValidadorResponsavel
    {
        private readonly IRepResponsavel _repResponsavel;
        private readonly IRepProcessoResponsavel _repProcessoResponsavel;

        public ValidadorResponsavel(IRepResponsavel repResponsavel, IRepProcessoResponsavel repProcessoResponsavel)
        {
            _repResponsavel = repResponsavel;
            _repProcessoResponsavel = repProcessoResponsavel;
        }


        public void ValidarCadastro(Responsavel resp)
        {
            resp.ValidarNome();
            resp.ValidarCpf();
            resp.ValidarEmail();
            ExcecaoSeTiverDuplicidade(resp);
        }

        private void ExcecaoSeTiverDuplicidade(Responsavel resp)
        {
            var existente = _repResponsavel.Recuperar()
                                           .Where(p => p.Cpf.Value == resp.Cpf.Value && p.Id != resp.Id)
                                           .Select(p => p.Nome)
                                           .FirstOrDefault();
            if (existente != null)
                throw new Exception(string.Format("O CPF já está em uso pelo responsável {0}.", existente));
        }


        public void ExcecaoSeTiverVinculo(Responsavel resp)
        {
            var existente = _repProcessoResponsavel.Recuperar()
                                           .Where(p => p.CodigoResponsavel == resp.Id)
                                           .OrderByDescending(p => p.Processo.Id)
                                           .Select(p => p.Processo.NumeroProcesso.Value)
                                           .ToList();
            if (existente.Any())
            {
                var msg = string.Format("O responsável não pode ser removido pois está vinculado ao processo {0}", NumeroProcesso.Formatar(existente.FirstOrDefault()));

                if (existente.Count > 1)
                    msg += ". Processos vinculados: " + existente.Count;

                throw new Exception(msg + ".");
            }
        }
    }
}
