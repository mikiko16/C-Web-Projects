using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MyAspNetProject.Data;
using MyAspNetProject.models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAspNetProject.Controllers
{
    [ApiController]
    [EnableCors("AllowAll")]
    [Route("team")]
    public class TeamBuildingController
    {
        private readonly ApplicationDbContext db;

        public TeamBuildingController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpPut]
        [Authorize]
        [Route("update/{id}")]
        public async Task<IEnumerable<UserApp>> UpdateUserState(string id)
        {
            var user = db.Users.FirstOrDefault(x => x.Id == id);
            user.IsActive = true;
            db.SaveChanges();

            var notActiveUsers = db.Users.Where(x => x.CompanyName == user.CompanyName && x.IsActive == false).ToList();

            await Execute();

            return notActiveUsers;
        }

        static async Task Execute()
        {
           // var apiKey = Environment.GetEnvironmentVariable("MySendGrid");
            var client = new SendGridClient("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");
            var from = new EmailAddress("test@example.com", "Example User");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("mikiko16@abv.bg", "Example User");
            var plainTextContent = "and Miro is the best !!!";
            var htmlContent = "<strong>Miro is the best !!!</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
