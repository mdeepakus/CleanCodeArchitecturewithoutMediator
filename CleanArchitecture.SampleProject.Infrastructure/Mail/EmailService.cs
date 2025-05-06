using CleanArchitecture.SampleProject.Application.Contracts.Infrastructure;
using CleanArchitecture.SampleProject.Application.Models.Mail;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CleanArchitecture.SampleProject.Infrastructure.Mail
{
    public class EmailService : IEmailService
    {
        public EmailSettings _emailSettings { get; }
        public ILogger<EmailService> _logger { get; }

        public EmailService(IOptions<EmailSettings> mailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = mailSettings.Value;
            _logger = logger;
        }

        /// <summary>
        /// sdfmksdlf
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<bool> SendEmail(Email email)
        {
            var sendGridClient = new SendGridClient(_emailSettings.ApiKey);
            var sendGridMessage = new SendGridMessage();
            sendGridMessage.SetFrom("deepak.mittal@hotmail.com", "Example");
            sendGridMessage.AddTo("deepak.mittal@instanda.com");
            //The Template Id will be something like this - d-9416e4bc396e4e7fbb658900102abaa2
            sendGridMessage.SetTemplateId("d-7d120772313644549dbec655f66ece6c");
            //Here is the Place holder values you need to replace.

            sendGridMessage.SetTemplateData(new
            {
                name = "Anuraj",
                Last_name = "Mittal",
                sender_Name = "Instanda",
                Sender_Email = "d@test.com",
                url = "https://dotnetthoughts.net"
            });


            var response = await sendGridClient.SendEmailAsync(sendGridMessage);

            //var subject = email.Subject;
            //var to = new EmailAddress(email.To);
            //var emailBody = email.Body;

            //var from = new EmailAddress
            //{
            //    Email = _emailSettings.FromAddress,
            //    Name = _emailSettings.FromName
            //};

            //var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
            //var response = await client.SendEmailAsync(sendGridMessage);

            _logger.LogInformation("Email sent");

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            _logger.LogError("Email sending failed");

            return false;
        }
    }
}
//deepak.mittal@hotmail.com
//TwilioPassword@2911