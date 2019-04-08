using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ClassWeb.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmail(string email, string subject, string body)
        {
            string Host = _configuration["Email:Host"];
            string Email = _configuration["Email:Email"];
            int Port = int.Parse(_configuration["Email:Port"]);
            string UserName = _configuration["Email:Email"];
            string Password = _configuration["Email:Password"];
            SmtpClient client = new SmtpClient(Host);
            client.Port = Port;
            client.UseDefaultCredentials = true;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(UserName,Password);

            MailMessage mailMessage = new MailMessage();
            mailMessage.IsBodyHtml = true;
            mailMessage.From = new MailAddress(Email);
            mailMessage.To.Add(email.ToString());
            mailMessage.Body = body;
            mailMessage.Subject =subject;
            client.Send(mailMessage);
            await Task.CompletedTask;
        }
    }
}

