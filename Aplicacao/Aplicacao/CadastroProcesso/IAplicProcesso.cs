using Aplicacao.Infra;

namespace Aplicacao.Aplicacao.CadastroProcesso
{
    public interface IAplicProcesso
    {
        RetornoPrepararEdicaoView PrepararEdicao(IdView view);
        RetornoPesquisaView Pesquisar(FiltroPesquisarProcessoView filtro);
        RetornoSalvarView Salvar(SalvarProcessoView view);
        void Remover(IdView view);
    }
}
