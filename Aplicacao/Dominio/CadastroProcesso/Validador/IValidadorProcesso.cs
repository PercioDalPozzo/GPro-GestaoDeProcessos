namespace Aplicacao.Dominio.CadastroProcesso
{
    public interface IValidadorProcesso
    {
        void ValidarCadastro(Processo processo);
        void ValidarExclusao(Processo processo);
    }
}
