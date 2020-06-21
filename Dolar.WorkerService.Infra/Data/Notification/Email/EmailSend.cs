using Dolar.WorkerService.Domain.Interfaces;
using Dolar.WorkerService.Domain.Models.ConfigureDependency;
using Dolar.WorkerService.Utils;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Dolar.WorkerService.Infra.Data.Notification.Email
{
    public class EmailSend : IEmailSend
    {
        private readonly ILogger<EmailSend> _logger;
        private readonly EmailSettings _emailSettings;
        public EmailSend(IOptions<EmailSettings> emailSettings,
            ILogger<EmailSend> logger)
        {
            this._logger = logger;
            this._emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(decimal dolarAntes, decimal dolarVlrNovo)
        {
            try
            {
                await Execute(CreateBodyEmail.BodyTrocaDeValor(dolarAntes, dolarVlrNovo));
            }
            catch (Exception e)
            {
                this._logger.LogError(e.InnerException != null ? e.InnerException.Message : e.Message);
            }
        }

        private async Task Execute(string message)
        {
            try
            {

                string toEmail = _emailSettings.ToEmail;

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.UsernameEmail, "João Paulo da Silva")
                };

                mail.To.Add(new MailAddress(toEmail));

                mail.Subject = "Valor Dolar - Alteração do valor do dolar";
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort))
                {
                    smtp.Credentials = new NetworkCredential(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception e)
            {
                this._logger.LogError(e.InnerException != null ? e.InnerException.Message : e.Message);
            }
        }
    }
}
