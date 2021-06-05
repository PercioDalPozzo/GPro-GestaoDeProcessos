using Aplicacao.Infra;
using System;

namespace Aplicacao.Dominio
{
    public class Responsavel : Entidade
    {
        public string Nome { get; set; }
        public Cpf Cpf { get; set; }
        public Email Email { get; set; }
        public byte[] Foto { get; set; }


        internal void ValidarNome()
        {
            if (string.IsNullOrEmpty(Nome))
                throw new Exception("Nome do responsável deve ser informado.");

            if (Nome.Length > 150)
                throw new Exception("Nome deve possuir no máximo 150 caracteres.");
        }

        internal void ValidarCpf()
        {
            if (string.IsNullOrEmpty(Cpf.Value))
                throw new Exception("CPF do responsável deve ser informado.");

            Cpf.Validar();
        }

        internal void ValidarEmail()
        {
            if (string.IsNullOrEmpty(Email.Value))
                throw new Exception("E-mail do responsável deve ser informado.");

            Email.Validar();
        }
    }
}
