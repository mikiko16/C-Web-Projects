using MyAspNetProject.Data;
using MyAspNetProject.models;
using MyAspNetProject.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAspNetProject.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext db;

        public UserService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<UserApp> GetInactiveUsers(string companyName)
        {
            return db.Users.Where(x => x.CompanyName == companyName && x.IsActive == false).ToList();
        }

        public UserApp GetUser(string id)
        {
            return db.Users.FirstOrDefault(x => x.Id == id);
        }

        public bool HasCompany(string companyName)
        {
            return db.Users.Any(x => x.CompanyName == companyName.ToUpper());
        }

        public UserApp UpdateState(string id)
        {
            var user = db.Users.FirstOrDefault(x => x.Id == id);
            user.IsActive = true;
            db.SaveChanges();

            return user;
        }
    }
}
