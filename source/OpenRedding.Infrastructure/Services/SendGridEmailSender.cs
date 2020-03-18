namespace OpenRedding.Infrastructure.Services
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity.UI.Services;

    public class SendGridEmailSender : IEmailSender
    {
        private readonly string _apiKey;

        public SendGridEmailSender(string apiKey) =>
            _apiKey = apiKey;

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            throw new NotImplementedException();
        }
    }
}
