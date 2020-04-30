using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyAspNetProject.Helpers
{
    public interface ICreateClaims
    {
        public Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password);
    }
}
