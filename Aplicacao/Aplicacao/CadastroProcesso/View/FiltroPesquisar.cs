using Aplicacao.Dominio.CadastroProcesso;
using Aplicacao.Infra;
using System;

namespace Aplicacao.Aplicacao.CadastroProcesso
{
    public class FiltroPesquisarView : FiltroPaginadoView
    {
        public string NumeroProcesso { get; set; }
        public DateTime? DataDistribuicaoIni { get; set; }
        public DateTime? DataDistribuicaoFim { get; set; }
        public bool ProcessoSegredo { get; set; }
        public string PastaFisica { get; set; }
        public EnumSituacaoProcesso? Situacao { get; set; }
        public string NomeResponsavel { get; set; }
    }
}
