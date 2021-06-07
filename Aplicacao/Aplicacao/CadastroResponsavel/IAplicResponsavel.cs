﻿using Aplicacao.Infra;

namespace Aplicacao.Aplicacao.CadastroResponsavel
{
    public interface IAplicResponsavel
    {
        RetornoPrepararEdicaoView PrepararEdicao(IdView view);
        RetornoPesquisaView Pesquisar(FiltroPesquisarView filtro);
        RetornoSalvarView Salvar(SalvarResponsavelView view);
        void Remover(IdView view);
    }
}
