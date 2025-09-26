using AutoGo.Application.Abstractions.Cloudinary;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace AutoGo.Infrastructure.Services.Cloudinary
{
    internal class CloudinaryServices : ICloudinaryServices
    {
        private readonly ICloudinary _cloudinary;
        private readonly ILogger<CloudinaryServices> _logger;
        private string folder = "VehiclesImages";
        public CloudinaryServices(IOptions<CloudinarySettings> options, ILogger<CloudinaryServices> logger)
        {
            _logger = logger;
            var account = new Account(
                options.Value.CloudName,
                options.Value.ApiKey,
                options.Value.ApiSecret
              );
            _cloudinary = new CloudinaryDotNet.Cloudinary(account);
           
        }

        public async Task<List<string>> AddVehicleImagesAsync(List<IFormFile> images)
        {
            try
            {
                var uploadedUrls = new List<string>();

                foreach (var image in images)
                {
                    if (image.Length<=0)
                    {
                        continue;
                    }

                    using (var stream = image.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams
                        {
                            File = new FileDescription(image.FileName, stream),
                            Folder = folder
                        };
                        var result = await _cloudinary.UploadAsync(uploadParams);
                        if (result.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            uploadedUrls.Add(result.SecureUrl.AbsoluteUri);
                        }
                    }
                }
                return uploadedUrls;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task DeleteImagesAsync(List<string> imageUrls)
        {
            try
            {
                foreach (var url in imageUrls)
                {
                    var id = GetPublicIdFromUrl(url);
                    if (!string.IsNullOrEmpty(id))
                    {
                        var parametrs = new DeletionParams($"{folder}/{id}");
                        await _cloudinary.DestroyAsync(parametrs);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
        private string GetPublicIdFromUrl(string url)
        {
            try
            {
                return url.Split('/').Last().Split('.').First();
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
