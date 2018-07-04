using SendGrid;
using SendGrid.Helpers.Mail;
using System.Configuration;
using System.Threading.Tasks;

namespace ContactWeb.Libraries
{
    public class Emailer
    {
        public static async Task Execute(EmailInputDto input)
        {
            var apiKey = ConfigurationManager.AppSettings["ContactWebSendgridKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(input.FromAddress, input.FromFormalName);
            var subject = input.Subject;
            var to = new EmailAddress(input.ToAddress, input.ToFormalName);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, null, input.Body);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
