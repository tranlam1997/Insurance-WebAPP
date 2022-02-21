using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace Sem3Project.Services
{
    public interface IMailService
    {
        Task SendMailAsync(string toEmail, string subject, string content);

        Task SendMailAsyncWithTemplate(string toEmail, object data, string templateId);
    }

    public class SendgridMailService : IMailService
    {
        private IConfiguration _configuration;

        public SendgridMailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMailAsync(string toEmail, string subject, string content)
        {
            var apiKey = _configuration["SendGridAPIKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("mrpsawn1996@gmail.com", "DoanDucBao");
            var to = new EmailAddress(toEmail);
            //var plainTextContent = "and easy to do anywhere, even with C#";
            //var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            //var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            var response = await client.SendEmailAsync(msg);
        }

        public async Task SendMailAsyncWithTemplate(string toEmail, object data, string templateId)
        {
            var apiKey = _configuration["SendGridAPIKey"];
            var client = new SendGridClient(apiKey);
            var sendGridMessage = new SendGridMessage();
            sendGridMessage.SetFrom("mrpsawn1996@gmail.com", "DoanDucBao");
            sendGridMessage.AddTo(toEmail);
            sendGridMessage.SetTemplateId(templateId);
            sendGridMessage.SetTemplateData(data);
            var response = await client.SendEmailAsync(sendGridMessage);
        }
    }
}
