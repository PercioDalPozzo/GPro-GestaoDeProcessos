using Aplicacao.Infra;

namespace Aplicacao.Aplicacao.CadastroResponsavel
{
    public class SalvarResponsavelView : SalvarView
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public byte[] Foto { get; set; }
        public bool AtualizarFoto { get; set; }
    }
}
