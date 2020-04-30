using Microsoft.AspNetCore.Identity;
using MyAspNetProject.JWT;
using MyAspNetProject.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyAspNetProject.Helpers
{
    public class CreateClaims : ICreateClaims
    {
        private readonly UserManager<UserApp> userManager;
        private readonly IJwtFactory _jwtFactory;
        UserApp userToVerify;

        public CreateClaims(UserManager<UserApp> userManager,
                            IJwtFactory jwtFactory)
        {
            this.userManager = userManager;
            this._jwtFactory = jwtFactory;
        }

        public async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
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
