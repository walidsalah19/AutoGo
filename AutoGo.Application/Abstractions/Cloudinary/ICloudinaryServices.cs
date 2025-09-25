using AutoGo.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Application.Abstractions.Cloudinary
{
    public interface ICloudinaryServices
    {
        public Task<List<string>> AddVehicleImagesAsync(List<IFormFile> images);
        Task DeleteImagesAsync(List<string> imageUrls);

    }
}
