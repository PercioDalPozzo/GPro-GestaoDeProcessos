using Aplicacao.Infra;

namespace Aplicacao.Aplicacao.CadastroResponsavel
{
    public interface IAplicResponsavel
    {
        RetornoPesquisaView Pesquisar(FiltroPesquisarView filtro);
        RetornoPrepararEdicaoView PrepararEdicao(IdView view);
        RetornoSalvarView Salvar(SalvarResponsavelView view);
        void Remover(IdView view);
    }
}
