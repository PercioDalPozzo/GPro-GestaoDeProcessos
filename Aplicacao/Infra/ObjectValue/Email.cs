using System;

namespace Aplicacao.Infra
{
    public class Email
    {


        public string Value { get; set; }

        public Email()
        {

        }

        public Email(string email)
        {
            Value = email;
        }

        internal void Validar()
        {
            if (Value.Length > 400)
                throw new Exception("E-mail deve possuir no máximo 400 caracteres.");

            if (!EhValido())
                throw new Exception(string.Format("E-mail inválido {0}.", Value));
        }

        bool EhValido()
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(Value);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
