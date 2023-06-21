using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;
using SendGrid;
using System.Net;

namespace Send_Emails_MVC.Models
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string subject, string email, string htmlMessage);
    }
    public class EmailSender : IEmailSender
    {
       
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }
      
        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager

        public Task SendEmailAsync(string subject, string email, string message)
        {
            return Execute(Options.SendGridKey, subject, email, message);
        }
        public async Task Execute(string apiKey, string subject, string email, string message)
        {
            var client = new SendGridClient(apiKey);
            //Add the verified sender idSendentity in the SendGrid portal. 
            var from = new EmailAddress("YourEmail@gmail.com", "Github Email sender ");
            var Subject = subject;
            var to = new EmailAddress("YourEmail@gmail.com", "Github Email sender  ");
            var plainTextContent = message;
            var htmlContent = message;
            var msg = MailHelper.CreateSingleEmail(from, to, Subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

            if (response.StatusCode != HttpStatusCode.OK
                && response.StatusCode != HttpStatusCode.Accepted)
            {
                var errorMessage = response.Body.ReadAsStringAsync().Result;
                throw new Exception($"Failed to send mail to {to}, status code {response.StatusCode}, {errorMessage}");
            }

        }
    }
}
