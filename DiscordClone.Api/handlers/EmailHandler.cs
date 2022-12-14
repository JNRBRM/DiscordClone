
using FluentEmail.Core;
using FluentEmail.Smtp;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace DiscordClone.Api.handlers
{
    public class EmailHandler
    {
        public async Task<bool> SendEmail(string recipientMail, string MailBody)
        {
            // Set up the SMTP server configuration
            using (var smtpServer = new SmtpClient
            {
                Host = "smtp.outlook.com",
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential("rasm232m@elev.tec.dk", "tnv73gmm")
            })
            {
                var msg = new MailMessage
                {
                    From = new MailAddress("rasm232m@elev.tec.dk"),
                    Subject = "Hello World",
                    Body = MailBody
                };
                msg.To.Add(new MailAddress(recipientMail));
                try
                {
                    smtpServer.Send(msg);
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
                
            }
        }
    }
}
