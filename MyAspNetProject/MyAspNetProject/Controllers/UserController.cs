using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyAspNetProject.Data;
using MyAspNetProject.models;
using MyAspNetProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public UserController(ApplicationDbContext db, 
                              UserManager<UserApp> userManager, 
                              SignInManager<UserApp> signInManager,
                              RoleManager<IdentityRole> roleManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
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

            var user = this.User;

            if (this.User.IsInRole("Admin"))
            {
                return BadRequest();
            }
            if (result.Succeeded)
            {
                return Ok(result);
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
                return Ok(result);
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
    }
}
