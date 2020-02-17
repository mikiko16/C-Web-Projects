using SharedTrip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SharedTrip.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext db;

        public UserService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void CreateUser(string username, string email, string password)
        {
            var user = new User
            {
                Email = email,
                Username = username,
                Password = this.Hash(password),
            };

            this.db.Users.Add(user);
            this.db.SaveChanges();
        }

        public string GetId(string username)
        {
            string userID = this.db.Users
                .Where(x => x.Username == username)
                .Select(x => x.Id)
                .FirstOrDefault();

            return userID;
        }

        public string GetUserId(string username, string password)
        {
            var passwordHash = this.Hash(password);
            return this.db.Users
                .Where(x => x.Username == username && x.Password == passwordHash)
                .Select(x => x.Id)
                .FirstOrDefault();
        }

        private string Hash(string input)
        {
            if (input == null)
            {
                return null;
            }

            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(input));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }

            return hash.ToString();
        }
    }
}
