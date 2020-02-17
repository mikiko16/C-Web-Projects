using Andreys.Data;
using Andreys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Andreys.Services
{
    public class UserService : IUserService
    {
        private readonly AndreysDbContext db;

        public UserService(AndreysDbContext db)
        {
            this.db = db;
        }
        public void CreateUser(string username, string password, string email)
        {
            var user = new User
            {
                Username = username,
                Password = this.Hash(password),
                Email = email
            };

            db.Users.Add(user);
            db.SaveChanges();
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
