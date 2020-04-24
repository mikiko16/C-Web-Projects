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
    public class TeamBuildingController : ControllerBase
    {
        private readonly ITeamBuildingService teambuildingService;
        private readonly UserManager<UserApp> _userManager;
        private readonly ClaimsPrincipal _caller;

        public TeamBuildingController(ITeamBuildingService teambuildingSerive,
                                      UserManager<UserApp> userManager,
                                      IHttpContextAccessor httpContextAccessor)
        {
            this.teambuildingService = teambuildingSerive;
            this._userManager = userManager;
            this._caller = httpContextAccessor.HttpContext.User;
        }

        [HttpPost]
        [Authorize(Policy = "ApiUser")]
        [Route("create")]
        public ActionResult<TeamBuilding> CreateTeamBuilding(TeamBuilding model)
        {
            UserApp user = _userManager.Users.FirstOrDefault(x => x.Id == model.CreatorId);
            string role = _caller.Claims.Single(c => c.Type == "Role").Value;

            if (!ModelState.IsValid || user == null)
            {
                return BadRequest("Something went wrong !");
            };

            if (role == "Admin")
            {
                return Ok(teambuildingService.CreateTeamBuilding(user.CompanyName, model));
            }

            return Ok();
        }

        [HttpGet]
        [Authorize(Policy = "ApiUser")]
        [Route("AllWithoutAdmin")]
        public async Task<IEnumerable<UserApp>> GetUsersFromCompany()
        {
            UserApp user = await _userManager.FindByIdAsync(_caller.Claims.Single(c => c.Type == "id").Value);

            return _userManager.Users.Where(x => x.CompanyName == user.CompanyName && x.Id != user.Id && x.IsActive == true).ToList();
        }

        [HttpGet]
        [Authorize(Policy = "ApiUser")]
        [Route("getActive")]
        public async Task<IEnumerable<TeamBuilding>> GetActiveTeambuildings()
        {
            UserApp user = await _userManager.FindByIdAsync(_caller.Claims.Single(c => c.Type == "id").Value);
       
            return teambuildingService.GetActiveTeambuildings(user);
        }

        [HttpGet]
        [Authorize(Policy = "ApiUser")]
        [Route("getPast")]
        public async Task<IEnumerable<TeamBuilding>> GetPastTeambuildings()
        {
            UserApp user = await _userManager.FindByIdAsync(_caller.Claims.Single(c => c.Type == "id").Value);

            return teambuildingService.GetPastTeambuildings(user);
        }

        [HttpGet]
        [Authorize(Policy = "ApiUser")]
        [Route("getById/{id}")]
        public TeamBuilding GetTeambuildingById(string id)
        {
            return teambuildingService.GetTeambuildingById(id);
        }

        [HttpGet]
        [Authorize(Policy = "ApiUser")]
        [Route("getPicsById/{id}")]
        public IEnumerable<Pictures> GetPictures(string id)
        {
            return teambuildingService.GetTeambuildingPics(id);
        }
    }
}
