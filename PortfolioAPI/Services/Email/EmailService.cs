using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using PortfolioAPI.Models.Email;

namespace PortfolioAPI.Services.Email;

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task<bool> SendEmail(EmailDTO emailDto)
    {
        try
        {
            //Declare message
            var message = new MimeMessage();
            //Declare and add from
            MailboxAddress from = new MailboxAddress("Web Portfolio", "harrihonkanenportfolio@outlook.com");
            message.From.Add(from);
            //Declare and add to
            MailboxAddress to = new MailboxAddress("Harri Honkanen", "devharri@outlook.com");
            message.To.Add(to);
            //Subject
            message.Subject = "New contact from Web Portfolio!";
            //Body
            TextPart bodyText = new TextPart("Plain");
            bodyText.Text = $"Sender name: {emailDto.Name}, phonenumber: {emailDto.Number}, email: {emailDto.Email}. " + $"Message content: {emailDto.Message}. ";
            message.Body = bodyText;

            using(var client = new SmtpClient())
            {

                client.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
                client.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
                var response = await client.SendAsync(message);
                Console.WriteLine(response);
                client.Disconnect(true);
            }

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}