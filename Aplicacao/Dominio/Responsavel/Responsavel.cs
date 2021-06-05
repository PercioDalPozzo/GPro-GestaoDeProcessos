using Aplicacao.Infra;

namespace Aplicacao.Dominio
{
    public class Responsavel : Entidade
    {
        public string Nome { get; set; }
        public Cpf Cpf { get; set; }
        public Email Email { get; set; }
        public byte[] Foto { get; set; }
    }
}
