namespace Aplicacao.Dominio.CadastroResponsavel
{
    public interface IValidadorResponsavel
    {
        void ValidarCadastro(Responsavel resp);
        void ExcecaoSeTiverVinculo(Responsavel resp);
    }
}
