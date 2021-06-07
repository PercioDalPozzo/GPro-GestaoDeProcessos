namespace Aplicacao.Dominio.CadastroProcesso
{
    public interface IValidadorProcesso
    {
        void ValidarCadastro(Processo resp);
        //void ExcecaoSeTiverVinculo(Processo resp);
    }
}
