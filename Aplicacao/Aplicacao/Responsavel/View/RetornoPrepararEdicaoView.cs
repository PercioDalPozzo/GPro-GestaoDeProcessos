using Aplicacao.Dominio;
using System;
using System.Collections.Generic;

namespace Aplicacao.Aplicacao
{
    public class RetornoPrepararEdicaoView
    {
        public RetornoPrepararEdicaoView()
        {
            Processos = new List<ProcessoView>();
        }

        public int Id { get; internal set; }
        public string Nome { get; internal set; }
        public string Cpf { get; internal set; }
        public string Email { get; internal set; }
        public byte[] Foto { get; internal set; }

        public List<ProcessoView> Processos { get; internal set; }
    }

    public class ProcessoView
    {
        public string Descricao { get; set; }
        public string NumeroProcesso { get; set; }
        public DateTime? DataDistribuicao { get; set; }
        public bool ProcessoSegredo { get; set; }
        public string PastaFisica { get; set; }
        public EnumSituacaoProcesso Situacao { get; set; }
        public string Responsaveis { get; set; }
    }

}
