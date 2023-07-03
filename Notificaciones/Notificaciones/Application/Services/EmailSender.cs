using Notificaciones.Application.Services.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace Notificaciones.Application.Common.Utils
{
    public class EmailSender : IMailService
    {
        public async Task<bool> SendEmail(string email)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("ailenadrianagomez@gmail.com", "Segunda Entrega");
            var subject = "La compra de la entrada fue realizada con exito!!";
            var to = new EmailAddress(email);
            var plainTextContent = "La compra de los asientos solicitados fue realizada con exito! Felicitaciones!";
            var htmlContent = "";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            return response.IsSuccessStatusCode == true ? true : false;
        }
    }
}
