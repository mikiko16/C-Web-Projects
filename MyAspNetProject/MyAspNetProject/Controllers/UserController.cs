using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyAspNetProject.Data;
using MyAspNetProject.Helpers;
using MyAspNetProject.models;
using MyAspNetProject.JWT;
using MyAspNetProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;

namespace MyAspNetProject.Controllers
{
    [EnableCors("AllowAll")]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<UserApp> userManager;
        private readonly SignInManager<UserApp> signInManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly ClaimsPrincipal _caller;
        UserApp userToVerify;
        public UserController(ApplicationDbContext db,
                              UserManager<UserApp> userManager,
                              SignInManager<UserApp> signInManager,
                              IJwtFactory jwtFactory,
                              IOptions<JwtIssuerOptions> jwtOptions,
                              IHttpContextAccessor httpContextAccessor,
                              RoleManager<IdentityRole> roleManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this._jwtFactory = jwtFactory;
            this._roleManager = roleManager;
            this._jwtOptions = jwtOptions.Value;
            this._caller = httpContextAccessor.HttpContext.User;
        }


        [HttpGet]
        [Authorize (Policy = "ApiUser")]
        [Route("AllFromCompany")]
        public async Task<IEnumerable<UserApp>> GetUsersFromCompany()
        {
            UserApp user = await userManager.FindByIdAsync(_caller.Claims.Single(c => c.Type == "id").Value);

            var users = userManager.Users
                .Where(x => x.CompanyName == user.CompanyName && x.IsActive == false).ToList();

            return users;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(LoginUser model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await this.signInManager.PasswordSignInAsync(model.Email,
                model.PasswordHash, true, false);

            var identity = await GetClaimsIdentity(model.Email, model.PasswordHash);

            if (result.Succeeded)
            {
                var isAdmin = await userManager.IsInRoleAsync(this.userToVerify, "Admin");
                var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, model.Email, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented }, isAdmin);
                return Ok(jwt);
            }

            return Unauthorized(result);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> PostRegister(UserApp model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            bool hasCompany = db.Users.Any(x => x.CompanyName == model.CompanyName.ToUpper());

            var newUser = new UserApp
            {
                Email = model.Email,
                UserName = model.Email,
                FirstName = model.FirstName,
                CompanyName = model.CompanyName.ToUpper(),
                LastName = model.LastName,
                EmailConfirmed = true,
                IsActive = !hasCompany
            };

            var result = await userManager.CreateAsync(newUser, model.PasswordHash);

            if (result.Succeeded)
            {
                if (!hasCompany)
                {
                    await userManager.AddToRoleAsync(newUser, "Admin");
                }
                else
                {
                    await userManager.AddToRoleAsync(newUser, "User");
                }

                return Ok();
            }
            return BadRequest(result);
        }

        [HttpPost]
        [Route("Logout")]

        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();

            return Ok();
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

            await SendEmail.Execute(user.Email, user.FirstName);

            return notActiveUsers;
        }

        [HttpDelete]
        [Authorize]
        [Route("delete/{id}")]
        public async Task<IEnumerable<UserApp>> DeleteUser(string id)
        {
            var user = db.Users.FirstOrDefault(x => x.Id == id);
            await userManager.DeleteAsync(user);
            db.SaveChanges();

            var allUsers = db.Users.Where(x => x.CompanyName == user.CompanyName && x.IsActive == false).ToList();

            return allUsers;
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            this.userToVerify = await userManager.FindByNameAsync(userName);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            // check the credentials
            if (await userManager.CheckPasswordAsync(userToVerify, password))
            {
                var isAdmin = await userManager.IsInRoleAsync(this.userToVerify, "Admin");
                if (isAdmin)
                {
                    return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id, "Admin"));
                }
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id, "User"));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}
