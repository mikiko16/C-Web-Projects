using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace MyAspNetProject.Helpers
{
    public class SendEmail
    {        
        public static async Task Execute(string email, string name)
        {
            var apiKey = Environment.GetEnvironmentVariable("MySendGrid");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("mikiko16@abv.bg", "Email for confirmation !");
            var subject = $"Welcome to our application, {name} !";
            var to = new EmailAddress(email, name);
            var plainTextContent = "and Miro is the best !!!";
            var htmlContent = "<strong>Your request has been approved! Enjoy our application!</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
