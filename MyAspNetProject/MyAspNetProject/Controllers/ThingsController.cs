using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyAspNetProject.Data;
using MyAspNetProject.models;
using MyAspNetProject.Models;
using MyAspNetProject.Services;
using MyAspNetProject.Services.Contracts;
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
        private readonly IThingService thingService;
        private readonly ClaimsPrincipal _caller;

        public ThingsController(IThingService thingService,
                                IHttpContextAccessor httpContextAccessor)
        {
            this.thingService = thingService;
            this._caller = httpContextAccessor.HttpContext.User;
        }

        public IHttpContextAccessor HttpContextAccessor { get; }

        [HttpPost]
        [Authorize(Policy = "ApiUser")]
        [Route("createThing")]
        public ActionResult<IEnumerable<ThingsNeeded>> createThing(ThingsNeeded model)
        {
            string role = _caller.Claims.Single(c => c.Type == "Role").Value;

            if(role == "Admin")
            {
                return Ok(thingService.CreateThing(model));
            }
            return Unauthorized();
        }

        [HttpGet]
        [Authorize(Policy = "ApiUser")]
        [Route("getThings/{id}")]
        public IEnumerable<ThingsNeeded> GetThingsForTeambuilding(string id)
        {
            return this.thingService.GetThing(id);
        }
    }
}
