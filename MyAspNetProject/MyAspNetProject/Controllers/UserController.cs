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
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly ClaimsPrincipal _caller;
        UserApp userToVerify;

        public UserController(ApplicationDbContext db,
                              UserManager<UserApp> userManager,
                              SignInManager<UserApp> signInManager,
                              RoleManager<IdentityRole> roleManager,
                              IJwtFactory jwtFactory,
                              IOptions<JwtIssuerOptions> jwtOptions,
                              IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this._jwtFactory = jwtFactory;
            this._jwtOptions = jwtOptions.Value;
            this._caller = httpContextAccessor.HttpContext.User;
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<IEnumerable<IdentityUser>> UpdateUserState(string id)
        {
            var user = db.Users.FirstOrDefault(x => x.Id == id);
            user.LockoutEnabled = true;
            db.SaveChanges();

            var notActiveUsers = db.Users.Where(x => x.LockoutEnabled == false).ToList();
            return notActiveUsers;
        }


        [HttpGet]
        [Authorize]
        [Route("All")]
        public ActionResult<IEnumerable<IdentityUser>> GetNotActiveUsers()
        {
            return db.Users.ToList();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(LoginUser model)
        {
            var result = await this.signInManager.PasswordSignInAsync(model.Email,
                model.PasswordHash, true, false);

            var identity = await GetClaimsIdentity(model.Email, model.PasswordHash);

            if (result.Succeeded)
            {
                //var user = await userManager.FindByNameAsync(model.Email);

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
            bool hasCompany = db.Users.Any(x => x.CompanyName == model.CompanyName);

            var newUser = new UserApp
            {
                Email = model.Email,
                UserName = model.Email,
                FirstName = model.FirstName,
                CompanyName = model.CompanyName,
                LastName = model.LastName,
                EmailConfirmed = true,
                IsActive = false
            };

            var result = await this.userManager.CreateAsync(newUser, model.PasswordHash);

            if (result.Succeeded)
            {
                if (!hasCompany)
                {
                    var res = await userManager.AddToRoleAsync(newUser, "Admin");
                    return Ok(res);
                }
                return new OkObjectResult("Account Created");
            }
            return BadRequest(result);
        }

        [HttpPost]
        [Authorize ( Policy = "ApiUser")]
        [Route("Logout")]

        public async Task<IActionResult> Logout()
        {
           // await this.signInManager.SignOutAsync();

            return Ok("Miro is the best !!!");
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
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}
