using Aplicacao.Dominio.CadastroProcesso;
using System;

namespace Aplicacao.Aplicacao.CadastroProcesso
{
    public class RetornoPesquisarView
    {
        public int Id { get; set; }
        public string NumeroProcesso { get; set; }
        public DateTime? DataDistribuicao { get; set; }
        public bool ProcessoSegredo { get; set; }
        public string PastaFisica { get; set; }
        public string Descricao { get; set; }
        public EnumSituacaoProcesso Situacao { get; set; }
        public string Responsaveis { get; set; }
    }
}
