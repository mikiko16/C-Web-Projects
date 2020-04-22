using MyAspNetProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAspNetProject.Services.Contracts
{
    public interface IAdService
    {
        public Task<Ad> UploadProfilePicture(Ad model, string id);

        public Task<IEnumerable<Ad>> GetAllAds();

        public Task<IEnumerable<Ad>> Delete(string id);
    }
}
