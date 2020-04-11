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
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyAspNetProject.Controllers
{
    [ApiController]
    [EnableCors("AllowAll")]
    [Route("things")]
    public class ThingsController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<UserApp> _userManager;
        private readonly ClaimsPrincipal _caller;

        public ThingsController(ApplicationDbContext db,
                                UserManager<UserApp> userManager,
                                IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this._userManager = userManager;
            this._caller = httpContextAccessor.HttpContext.User;
        }

        [HttpPost]
        [Authorize(Policy = "ApiUser")]
        [Route("createThing")]
        public async Task<IEnumerable<ThingsNeeded>> createThing(ThingsNeeded model)
        {
            //UserApp user = await _userManager.FindByIdAsync(_caller.Claims.Single(c => c.Type == "id").Value);

            ThingsNeeded thing = new ThingsNeeded
            {
                Name = model.Name,
                TeamBuildingId = model.TeamBuildingId,
                UserAppId = model.UserAppId
            };

            db.ThingsNedded.Add(thing);
            db.SaveChanges();

            var things = db.ThingsNedded.Where(x => x.TeamBuildingId == model.TeamBuildingId && x.UserAppId == model.UserAppId);

            return things;
        }

        [HttpPost]
       // [Authorize(Policy = "ApiUser")]
        [Route("uploadImage")]
        public async Task<string> UploadProfilePicture(IFormFile Image)
        {
            if (Image == null || Image.Length == 0)
                throw new Exception("Please select profile picture");

            var folderName = Path.Combine("Resources", "ProfilePics");
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            var uniqueFileName = "_profilepic.png";
            var dbPath = Path.Combine(folderName, uniqueFileName);

            using (var fileStream = new FileStream(Path.Combine(filePath, uniqueFileName), FileMode.Create))
            {
                await Image.CopyToAsync(fileStream);
            }

            return dbPath;
        }
    }
}
