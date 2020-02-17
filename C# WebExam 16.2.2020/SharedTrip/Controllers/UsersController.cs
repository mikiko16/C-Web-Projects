using SharedTrip.Services;
using SharedTrip.ViewModels.Users;
using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Controllers
{
    public class UsersController : Controller
    {
        private IUserService usersService;

        public UsersController(IUserService usersService)
        {
            this.usersService = usersService;
        }
        public HttpResponse Login()
        {
            return this.View();
        }

        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(string username, string password)
        {
            var userId = this.usersService.GetUserId(username, password);

            if (userId == null)
            {
                return this.Redirect("/Users/Login");
            }
            this.SignIn(userId);
            return this.Redirect("/");
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel input)
        {
            if (input.Password != input.ConfirmPassword)
            {
                return this.Error("Passwords should be the same!");
            }

            if (input.Username?.Length < 5 || input.Username?.Length > 20)
            {
                return this.Error("Username should be between 5 and 20 characters .");
            }

            if (input.Password?.Length < 6 || input.Password?.Length > 20)
            {
                return this.Error("Password should be between 6 and 20 characters.");
            }

            //if (!IsValid(input.Email))
            //{
            //    return this.Error("Invalid email!");
            //}

            //if (this.usersService.IsUsernameUsed(input.Username))
            //{
            //    return this.Error("Username already used!");
            //}
            //
            //if (this.usersService.IsEmailUsed(input.Email))
            //{
            //    return this.Error("Email already used!");
            //}

            this.usersService.CreateUser(input.Username, input.Email, input.Password);
            return this.Redirect("/Users/Login");
        }
    }
}
