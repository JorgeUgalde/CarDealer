using Microsoft.AspNetCore.Identity.UI.Services;

namespace CarDealer.Utilities
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //Funcionalidad de envío de correos

            return Task.CompletedTask;
        }
    }
}
