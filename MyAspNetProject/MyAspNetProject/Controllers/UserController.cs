using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyAspNetProject.Data;
using MyAspNetProject.Models;
using System;
using System.Threading.Tasks;

namespace MyAspNetProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController //: BaseController<User, IdentityRole, ApplicationDbContext>
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserController(ApplicationDbContext db, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.db = db;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        [HttpPost]
        [Route("Register")]
        //POST : /api/User/Register
        public async Task<Object> PostApplicationUser(User model)
        {
            var user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password,
                Email = model.Email,
                CompanyName = model.CompanyName,
                UserName = model.UserName
            };

            try
            {
                var result = await _userManager.CreateAsync(user, model.Password);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
