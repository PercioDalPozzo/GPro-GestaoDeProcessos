using Aplicacao.Dominio.CadastroProcesso;
using Aplicacao.Infra;
using System;
using System.Collections.Generic;

namespace Aplicacao.Aplicacao.CadastroProcesso
{
    public class SalvarProcessoView : SalvarView
    {
        public SalvarProcessoView()
        {
            ProcessoResponsavel = new List<SalvarProcessoResponsavelView>();
        }
        public string NumeroProcesso { get; set; }
        public DateTime? DataDistribuicao { get; set; }
        public bool ProcessoSegredo { get; set; }
        public string PastaFisica { get; set; }
        public string Descricao { get; set; }
        public EnumSituacaoProcesso Situacao { get; set; }
        public int? CodigoProcessoPai { get; set; }
        public List<SalvarProcessoResponsavelView> ProcessoResponsavel { get; set; }
    }

    public class SalvarProcessoResponsavelView : SalvarView
    {
        public int CodigoResponsavel { get; set; }
    }
}
