using Aplicacao.Infra;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aplicacao.Dominio.CadastroProcesso
{
    public class Processo : Entidade
    {
        public Processo()
        {
            ProcessoResponsavel = new List<ProcessoResponsavel>();
        }

        public string Descricao { get; set; }
        public NumeroProcesso NumeroProcesso { get; set; }
        public DateTime? DataDistribuicao { get; set; }
        public bool ProcessoSegredo { get; set; }
        public string PastaFisica { get; set; }
        public EnumSituacaoProcesso Situacao { get; set; }
        public int? CodigoProcessoPai { get; set; }
        public virtual Processo ProcessoPai { get; set; }
        public virtual List<Processo> ProcessoFilho { get; set; }
        public virtual List<ProcessoResponsavel> ProcessoResponsavel { get; set; }


        internal void SetarProcessoPai(Processo processo)
        {
            ProcessoPai = processo;
            CodigoProcessoPai = processo.Id;
        }

        public bool Finalizado()
        {
            return (Situacao == EnumSituacaoProcesso.Finalizado) || (Situacao == EnumSituacaoProcesso.Arquivado);
        }


        internal void ValidarDataDistribuicao()
        {
            if ((DataDistribuicao.HasValue) && (DataDistribuicao > DateTime.Today))
                throw new Exception("Data de distribuição se informada, deve ser menor ou igual a data atual.");
        }

        internal void ValidarResponsaveis()
        {
            if (!ProcessoResponsavel.Any())
                throw new Exception("O processo deve possuir ao menos um responsável.");

            if (ProcessoResponsavel.Count() > 3)
                throw new Exception("O processo deve possuir até 3 responsáveis.");

            var duplicado = ProcessoResponsavel.GroupBy(p => new { p.CodigoResponsavel, p.Responsavel.Nome }).FirstOrDefault(p => p.Count() > 1);
            if (duplicado != null)
                throw new Exception(string.Format("O responsável {0} está duplicado no processo.", duplicado.Key.Nome));
        }

        internal void ValidarPastaFisica()
        {
            if (string.IsNullOrEmpty(PastaFisica))
                return;

            if (PastaFisica.Length > 50)
                throw new Exception("Pasta física se informada, deve possuir no máximo 50 caracteres.");
        }

        internal void ValidarDescricao()
        {
            if (string.IsNullOrEmpty(Descricao))
                return;

            if (Descricao.Length > 1000)
                throw new Exception("Descrição se informada, deve possuir no máximo 1000 caracteres.");
        }

        internal void ValidarNumeroProcesso()
        {
            if (string.IsNullOrEmpty(NumeroProcesso.Value))
                throw new Exception("Numero do processo unificado deve ser informado.");

            NumeroProcesso.Validar();
        }
    }
}
