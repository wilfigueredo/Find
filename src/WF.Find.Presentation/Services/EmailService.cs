using SendGrid;
using System.Net;
using System.Threading.Tasks;
using SendGrid.SmtpApi;
using Microsoft.AspNet.Identity;
using SendGrid.Helpers.Mail;

namespace WF.Find.Presentation.Services
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            //return ConfigSendGridasync(message);
            return EnviaEmailAsync(message);
        }
        public static Task EnviaEmailAsync(IdentityMessage messagem)
        {
            // serviço para enviar email
            var minhaMensagem = new SendGrid.SendGridMessage();
            minhaMensagem.AddTo(messagem.Destination);
            minhaMensagem.From = new System.Net.Mail.MailAddress("williamfigueredo@outlook.com", "Find");
            minhaMensagem.Subject = messagem.Subject;
            minhaMensagem.Text = messagem.Body;
            minhaMensagem.Html = messagem.Body;
            var credenciais = new NetworkCredential("FPBFDV", "paraiba2002");
            // Cria um transporte web para enviar email
            var transporteWeb = new Web(credenciais);
            // Envia o email
            if (transporteWeb != null)
            {
                return transporteWeb.DeliverAsync(minhaMensagem);
            }
            else
            {
                return Task.FromResult(0);
            }
        }
    }
}