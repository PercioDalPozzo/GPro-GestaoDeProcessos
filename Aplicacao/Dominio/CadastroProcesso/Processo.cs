using Aplicacao.Infra;
using System;
using System.Collections.Generic;

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
        public virtual List<ProcessoResponsavel> ProcessoResponsavel { get; set; }



        public bool Finalizado()
        {
            return (Situacao == EnumSituacaoProcesso.Finalizado) || (Situacao == EnumSituacaoProcesso.Arquivado);
        }
    }
}
