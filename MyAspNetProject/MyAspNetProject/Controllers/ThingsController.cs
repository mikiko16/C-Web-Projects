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

        public ThingsController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpPost]
        [Authorize(Policy = "ApiUser")]
        [Route("createThing")]
        public async Task<IEnumerable<ThingsNeeded>> createThing(ThingsNeeded model)
        {
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

        [HttpGet]
        [Authorize(Policy = "ApiUser")]
        [Route("getThings/{id}")]
        public async Task<IEnumerable<ThingsNeeded>> GetThingsForTeambuilding(string id)
        {
            var things = db.ThingsNedded.Where(x => x.TeamBuildingId == id);

            return things;
        }
    }
}
