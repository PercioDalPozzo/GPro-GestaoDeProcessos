﻿using Aplicacao.Infra;

namespace Aplicacao.Aplicacao.CadastroResponsavel
{
    public class FiltroPesquisarView : FiltroPaginadoView
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string NumeroProcesso { get; set; }
    }
}