using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyAspNetProject.Data;
using MyAspNetProject.models;
using MyAspNetProject.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyAspNetProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public UserController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<IEnumerable<UserApp>> UpdateUserState(string id)
        {
            var user = db.Users.FirstOrDefault(x => x.Id == id);
            user.IsActive = true;
            db.SaveChanges();

            var notActiveUsers = db.Users.Where(x => x.IsActive == false).ToList();
            return notActiveUsers;
        }


        [HttpGet]
        [Route("All")]
        public ActionResult<IEnumerable<UserApp>> GetNotActiveUsers()
        {
            return db.Users.Where(x => x.IsActive == false).ToList();
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult<UserApp> Get(LoginUser model)
        {
            var user = this.db.Users.FirstOrDefault(x => x.Email == model.Email);

            if (user == null)
            {
                return this.NotFound();
            }
            if (user.PasswordHash != model.PasswordHash)
            {
                return this.BadRequest();
            }

            return user;
        }

        [HttpPost]
        [Route("Register")]
        public ActionResult<UserApp> PostRegister(UserApp model)
        {
            this.db.Users.Add(model);
            this.db.SaveChanges();
            return this.Created($"api/User/Register", model);
        }
    }
}
