using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MyAspNetProject.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAspNetProject.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        Cloudinary cloudinary = new Cloudinary(new Account(
             "mikiko16",
             "686516265985614",
             "cyjH_KBR9Djp3oOQhUWGcKr3FWg"));
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
