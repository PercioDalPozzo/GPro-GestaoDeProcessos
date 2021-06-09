using System.Collections.Generic;

namespace Aplicacao.EnvioEmail
{
    public class EnviarEmailView
    {
        public EnviarEmailView()
        {
            Destinatarios = new List<string>();
        }

        public List<string> Destinatarios { get; set; }
        public string Assunto { get; set; }
        public string Msg { get; set; }
    }
}
