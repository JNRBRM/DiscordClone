
using FluentEmail.Core;
using FluentEmail.Smtp;
using System.Net.Mail;
using System.Text;

namespace DiscordClone.Api.handlers
{
    public class EmailHandler
    {
        public async Task<bool> SendEmail(string body)
        {
            var sender = new SmtpSender(() => new SmtpClient("localhost")
            {
                EnableSsl = false,
                //DeliveryMethod = SmtpDeliveryMethod.Network,
                Port = 25,
                DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                PickupDirectoryLocation = @"C:\Demos"
            });


            Email.DefaultSender = sender;
            //Email.DefaultRenderer = new RazorRenderer();

            var email = await Email
                .From("tim@timco.com")
                .To("test@test.com", "Sue")
                .Subject("Thanks!")
                .UsingTemplate(body.ToString(), new { FirstName = "Tim", ProductName = "Bacon-Wrapped Bacon" })
                //.Body("Thanks for buying our product.")
                .SendAsync();
            return true;
        }
    }
}
