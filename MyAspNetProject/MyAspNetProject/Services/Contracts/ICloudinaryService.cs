using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MyAspNetProject.Services.Contracts
{
    public interface ICloudinaryService
    {
        public ImageUploadResult Upload(ImageUploadParams data);

        public DelResResult Delete(string data);
    }
}
