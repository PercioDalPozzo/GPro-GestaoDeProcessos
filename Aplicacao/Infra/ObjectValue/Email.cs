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

            if (!EhValido())
                throw new Exception(string.Format("E-mail inválido {0}.", Value));
        }

        public bool EhValido()
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
