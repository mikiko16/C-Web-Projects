using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Services
{
    public interface IUserService
    {
        void CreateUser(string username, string email, string password);

        string GetUserId(string username, string password);

        string GetId(string username);
    }
}
