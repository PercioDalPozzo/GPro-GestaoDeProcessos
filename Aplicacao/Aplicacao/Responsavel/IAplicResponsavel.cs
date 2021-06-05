using Aplicacao.Infra;

namespace Aplicacao.Aplicacao
{
    public interface IAplicResponsavel
    {
        RetornoPesquisaView Pesquisar(FiltroPesquisarView filtro);
        RetornoPrepararEdicaoView PrepararEdicao(IdView view);
        RetornoSalvarView Salvar(SalvarResponsavelView view);
    }
}
