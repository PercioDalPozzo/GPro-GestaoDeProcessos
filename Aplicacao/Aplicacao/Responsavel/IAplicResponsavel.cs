using System.Collections.Generic;

namespace Aplicacao.Aplicacao.Responsavel
{
    public interface IAplicResponsavel
    {
        List<RetornoPesquisarView> Pesquisar(FiltroPesquisarView filtro);
    }
}
