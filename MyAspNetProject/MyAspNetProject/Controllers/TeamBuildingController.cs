using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<UserApp> _userManager;

        public TeamBuildingController(ApplicationDbContext db, UserManager<UserApp> userManager)
        {
            this.db = db;
            this._userManager = userManager;
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

            await Execute(user.Email, user.FirstName);

            return notActiveUsers;
        }

        [HttpDelete]
        [Authorize]
        [Route("delete/{id}")]
        public async Task<IEnumerable<UserApp>> DeleteUser(string id)
        {
            var user = db.Users.FirstOrDefault(x => x.Id == id);
            await _userManager.DeleteAsync(user);
            db.SaveChanges();

            var allUsers = db.Users.Where(x => x.CompanyName == user.CompanyName && x.IsActive == false).ToList();

            return allUsers;
        }

        static async Task Execute(string email, string name)
        {
           // var apiKey = Environment.GetEnvironmentVariable("MySendGrid");
            var client = new SendGridClient("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");
            var from = new EmailAddress(email, "Email for confirmation !");
            var subject = $"Welcome to out application, {name} !";
            var to = new EmailAddress("mikiko16@abv.bg", name);
            var plainTextContent = "and Miro is the best !!!";
            var htmlContent = "<strong>Your request has been approved! Enjoy our application!</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
