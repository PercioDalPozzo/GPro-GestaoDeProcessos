using Aplicacao.Infra;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacao.Aplicacao.Responsavel
{
    public class FiltroPesquisarView : FiltroPaginadoView
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string NumeroProcesso { get; set; }
    }
}
