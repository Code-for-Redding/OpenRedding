namespace OpenRedding.Infrastructure.Services
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using SendGrid;
    using SendGrid.Helpers.Mail;

    public class SendGridEmailSender : IEmailSender
    {
        private const string OpenReddingEmail = "joey.mckenzie27@gmail.com";
        private readonly string _apiKey;

        public SendGridEmailSender(string apiKey) =>
            _apiKey = apiKey;

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress(OpenReddingEmail);
            var to = new EmailAddress(email);
            const string plainTextContent = "Please click the following link to reset your password. Do not reply to this email.";
            var message = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlMessage);
            await client.SendEmailAsync(message).ConfigureAwait(false);
        }
    }
}
