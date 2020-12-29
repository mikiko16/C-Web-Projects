using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyAspNetProject.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAspNetProject.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private Cloudinary cloudinary;
        public CloudinaryService([FromServices] Cloudinary _cloudinary)
        {
            this.cloudinary = _cloudinary;
        }
        public ImageUploadResult Upload(ImageUploadParams data)
        {
            return cloudinary.Upload(data);
        }

        public DelResResult Delete(string data)
        {
            return cloudinary.DeleteResourcesByTag(data);
        }
    }
}
