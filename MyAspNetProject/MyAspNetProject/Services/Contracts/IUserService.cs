using MyAspNetProject.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAspNetProject.Services.Contracts
{
    public interface IUserService
    {
        public bool HasCompany(string companyName);

        public UserApp UpdateState(string id);

        public UserApp GetUser(string id);

        public List<UserApp> GetInactiveUsers(string companyName);
    }
}
