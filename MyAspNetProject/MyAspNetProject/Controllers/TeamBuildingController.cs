using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyAspNetProject.Data;
using MyAspNetProject.models;
using MyAspNetProject.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private readonly ClaimsPrincipal _caller;

        public TeamBuildingController(ApplicationDbContext db,
                                      UserManager<UserApp> userManager,
                                      IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this._userManager = userManager;
            this._caller = httpContextAccessor.HttpContext.User;
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

        [HttpPost]
        [Authorize(Policy = "ApiUser")]
        [Route("create")]
      
        public async Task<TeamBuilding> CreateTeamBuilding(TeamBuilding model)
        {
            UserApp user = await _userManager.FindByIdAsync(model.CreatorId);

            var teamBuilding = new TeamBuilding
            {
                CompanyName = user.CompanyName,
                Location = model.Location,
                CreatorId = model.CreatorId,
                Date = model.Date
            };

            var team = db.TeamBuilding.Add(teamBuilding);
            await db.SaveChangesAsync();

            return teamBuilding;
        }

        [HttpGet]
        [Authorize(Policy = "ApiUser")]
        [Route("AllWithoutAdmin")]
        public async Task<IEnumerable<UserApp>> GetUsersFromCompany()
        {
            UserApp user = await _userManager.FindByIdAsync(_caller.Claims.Single(c => c.Type == "id").Value);

            var users = _userManager.Users
                .Where(x => x.CompanyName == user.CompanyName && x.Id != user.Id && x.IsActive == true).ToList();

            return users;
        }

        [HttpGet]
        [Authorize(Policy = "ApiUser")]
        [Route("getActive")]
        public async Task<IEnumerable<TeamBuilding>> GetActiveTeambuildings()
        {
            UserApp user = await _userManager.FindByIdAsync(_caller.Claims.Single(c => c.Type == "id").Value);

            var teambuilds = db.TeamBuilding.Where(x => x.CompanyName == user.CompanyName && x.Date > DateTime.Now);
       
            return teambuilds;
        }

        [HttpGet]
        [Authorize(Policy = "ApiUser")]
        [Route("getPast")]
        public async Task<IEnumerable<TeamBuilding>> GetPastTeambuildings()
        {
            UserApp user = await _userManager.FindByIdAsync(_caller.Claims.Single(c => c.Type == "id").Value);

            var teambuilds = db.TeamBuilding.Where(x => x.CompanyName == user.CompanyName && x.Date < DateTime.Now);

            return teambuilds;
        }

        [HttpGet]
        [Authorize(Policy = "ApiUser")]
        [Route("getById/{id}")]
        public async Task<TeamBuilding> GetTeambuildingById(string id)
        {
            var teambuild = db.TeamBuilding.FirstOrDefault(x => x.Id == id);

            return teambuild;
        }

        static async Task Execute(string email, string name)
        {
           // var apiKey = Environment.GetEnvironmentVariable("MySendGrid");
            var client = new SendGridClient("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");
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
