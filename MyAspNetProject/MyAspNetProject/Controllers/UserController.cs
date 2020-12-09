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
using MyAspNetProject.TestCFolder;
using Microsoft.Data.SqlClient;
using System.Data;
using WebPush;

namespace MyAspNetProject.Controllers
{
    [EnableCors("AllowAll")]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<UserApp> userManager;
        private readonly SignInManager<UserApp> signInManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ICreateClaims createClaims;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly ClaimsPrincipal _caller;
        UserApp userToVerify;
        public UserController(ApplicationDbContext db,
                              UserManager<UserApp> userManager,
                              SignInManager<UserApp> signInManager,
                              IJwtFactory jwtFactory,
                              IOptions<JwtIssuerOptions> jwtOptions,
                              IHttpContextAccessor httpContextAccessor,
                              RoleManager<IdentityRole> roleManager,
                              ICreateClaims createClaims)
        {
            this.db = db;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this._jwtFactory = jwtFactory;
            this._roleManager = roleManager;
            this.createClaims = createClaims;
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
        public async Task<IActionResult> Login(LoginUser model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Something is missing !");
            }
            var result = await this.signInManager.PasswordSignInAsync(model.Email,
                model.PasswordHash, true, false);

            var identity = await createClaims.GetClaimsIdentity(model.Email, model.PasswordHash);

            this.userToVerify = await userManager.FindByNameAsync(model.Email);
            if (result.Succeeded)
            {
                var isAdmin = await userManager.IsInRoleAsync(this.userToVerify, "Admin");
                var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, model.Email, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented }, isAdmin);
                return Ok(jwt);
            }

            return Unauthorized("Wrong password or username !");
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
                VapidDetails vapidDetails = new VapidDetails();
                vapidDetails.PrivateKey = "tUucEQMGJcB19FSboALLmUEZXW8897x8nfOm158ocCA";
                vapidDetails.PublicKey = "BD525odruiNj8jDtwGNcU7jiaCXUQqM2R1TL4e7GqvFj48kls8iIp9i5zkIXgcu5_5ym_6guLIR4khwTKVKncV0";
                vapidDetails.Subject = "http://localhost:4200";

                var message = new NotificationModel();
                message.Title = "New User Registered";
                message.Message = "Please confirm my request !!!";
                message.Url = "Without URL";
                var client = new WebPushClient();
                var serializedMessage = JsonConvert.SerializeObject(message);

                foreach (var pushSubscription in Subscription.Subscriptions)
                {
                    await client.SendNotificationAsync(pushSubscription, serializedMessage, vapidDetails);
                }
                return Ok();
            }
            return BadRequest(result);
        }

        [HttpPost]
        [Route("Logout")]

        public async Task<ActionResult> Logout()
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
    }
}
