using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Aplicacao.EnvioEmail
{
    public class ServEmail : IServEmail
    {
        private IConfiguration _configuration;

        public ServEmail(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task EnviarEmail(EnviarEmailView view)
        {
            try
            {
                Execute(view).Wait();
                return Task.FromResult(0);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Execute(EnviarEmailView view)
        {
            try
            {
                var primaryDomain = _configuration.GetSection("ConfigEnvioEmail")["PrimaryDomain"];
                var primaryPort = _configuration.GetSection("ConfigEnvioEmail")["PrimaryPort"];
                var usernameEmail = _configuration.GetSection("ConfigEnvioEmail")["UsernameEmail"];
                var usernamePassword = _configuration.GetSection("ConfigEnvioEmail")["UsernamePassword"];


                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(usernameEmail, "GPro - Gestão de processos")
                };

                foreach (var dest in view.Destinatarios)
                {
                    //mail.To.Add(new MailAddress(view.Destinatarios.FirstOrDefault()));
                    mail.CC.Add(new MailAddress(view.Destinatarios.FirstOrDefault()));
                }



                mail.Subject = view.Assunto;
                mail.Body = view.Msg;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                //outras opções
                //mail.Attachments.Add(new Attachment(arquivo));
                //

                using (SmtpClient smtp = new SmtpClient(primaryDomain, int.Parse(primaryPort)))
                {
                    smtp.Credentials = new NetworkCredential(usernameEmail, usernamePassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
