namespace Aplicacao.Dominio
{
    public interface IValidadorResponsavel
    {
        void ValidarCadastro(Responsavel resp);
        void ExcecaoSeTiverVinculo(Responsavel resp);
    }
}
