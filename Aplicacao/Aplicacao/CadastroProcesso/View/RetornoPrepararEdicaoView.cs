using Aplicacao.Dominio.CadastroProcesso;
using System;
using System.Collections.Generic;

namespace Aplicacao.Aplicacao.CadastroProcesso
{
    public class RetornoPrepararEdicaoView
    {
        public RetornoPrepararEdicaoView(Processo processo)
        {
            Id = processo.Id;
            NumeroProcesso = processo.NumeroProcesso.Formatar();
            DataDistribuicao = processo.DataDistribuicao;
            ProcessoSegredo = processo.ProcessoSegredo;
            PastaFisica = processo.PastaFisica;
            Descricao = processo.Descricao;
            Situacao = processo.Situacao;
            CodigoProcessoPai = processo.CodigoProcessoPai;
        }

        public int Id { get; set; }
        public string NumeroProcesso { get; set; }
        public DateTime? DataDistribuicao { get; set; }
        public bool ProcessoSegredo { get; set; }
        public string PastaFisica { get; set; }
        public string Descricao { get; set; }
        public EnumSituacaoProcesso Situacao { get; set; }

        public int? CodigoProcessoPai { get; set; }

        public RetornoPrepararEdicaoView ProcessoPai { get; set; }
        public RetornoPrepararEdicaoView ProcessoFilho { get; set; }
        public RetornoPrepararEdicaoView ProcessoNeto { get; set; }

        public List<RetornoPrepararEdicaoResponsavelView> ProcessoResponsavel { get; set; }
    }

    public class RetornoPrepararEdicaoResponsavelView
    {
        public RetornoPrepararEdicaoResponsavelView(ProcessoResponsavel processoResponsavel)
        {
            Id = processoResponsavel.Id;
            Nome = processoResponsavel.Responsavel.Nome;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
