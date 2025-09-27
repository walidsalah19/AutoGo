using AutoGo.Application.Abstractions.Cashing;
using AutoGo.Application.Abstractions.Cloudinary;
using AutoGo.Application.Common.Events.VehicleEvent.CreateVehicle;
using AutoGo.Application.Common.Result;
using AutoGo.Application.Vehicles.Commands.AddVehicle;
using AutoGo.Application.Vehicles.Dto;
using AutoGo.Domain.Interfaces.UnitofWork;
using AutoGo.Domain.Models;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGo.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace AutoGo.Application.Vehicles.Commands.UpdateVehicle
{
    public class UpdateVehicleHnadler : IRequestHandler<UpdateVehicleCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AddVehicleHandler> _logger;
        private readonly IVehicleCacheService _cacheService;
        private readonly IMapper _mapper;
        private readonly ICloudinaryServices _cloudinaryServices;


        public UpdateVehicleHnadler(IUnitOfWork unitOfWork, ILogger<AddVehicleHandler> logger, IVehicleCacheService cacheService, IMapper mapper, ICloudinaryServices cloudinaryServices)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _cacheService = cacheService;
            _mapper = mapper;
            _cloudinaryServices = cloudinaryServices;
        }

        public async Task<Result<string>> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var vehicle =await _unitOfWork.Repository<Vehicle>().FindEntityById(request.Id);
                if (vehicle == null)
                    return Result<string>.Failure(new Error(message: "check vehicle data please ",
                        code: (int)ErrorCodes.BadRequest));


                UpdateVehicleFields(vehicle,request);
                await _unitOfWork.Repository<Vehicle>().UpdateAsync(vehicle);
                await _unitOfWork.CompleteAsync();

                var vehicleDto = _mapper.Map<VehicleDto>(vehicle);

                vehicleDto.Images = await SaveVehicleInDb(vehicle, request.Images);


                await _cacheService.UpdateAllFieldAsync(vehicleDto);

                return Result<string>.Success("Updating vehicle successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
        private async Task<List<string>> SaveVehicleInDb(Vehicle vehicle, List<IFormFile> images)
        {
            try
            {
                // 2️⃣ ارفع الصور على Cloudinary
                var imageDb = await _unitOfWork.Repository<VehicleImage>().GetRange(x=>x.VehicleId.Equals(vehicle.Id));
                var imagePath =await imageDb.Select(x => x.Url).ToListAsync();
                var imageUrls = await MangeCloudinaryImage(imagePath, images);


                //
                await _unitOfWork.Repository<VehicleImage>().RemoveRange(await imageDb.ToListAsync());

                // 3️⃣ أنشئ قائمة الصور
                var imageEntities = imageUrls.Select(url => new VehicleImage
                {
                    Url = url,
                    VehicleId = vehicle.Id
                }).ToList();

                await _unitOfWork.Repository<VehicleImage>().AddRangeAsync(imageEntities);
                await _unitOfWork.CompleteAsync();

                return imageUrls;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw; // أعد رمي الاستثناء عشان الـ Middleware يعالجه
            }
        }

        private async Task<List<string>> MangeCloudinaryImage(List<string> imageUrl, List<IFormFile> images)
        {
            try
            {
                await _cloudinaryServices.DeleteImagesAsync(imageUrl);
                var imageUrls = await _cloudinaryServices.AddVehicleImagesAsync(images);

                return imageUrls; 
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
        private void UpdateVehicleFields(Vehicle vehicle, UpdateVehicleCommand request)
        {
            vehicle.UpdatedAt = DateTime.UtcNow;
            vehicle.Category = request.Category;
            vehicle.DailyRate = request.DailyRate;
            vehicle.LicensePlate = request.LicensePlate;
            vehicle.Make = request.Make;
            vehicle.Color = request.Color;
            vehicle.Model = request.Model;
            vehicle.Year = request.Year;
            vehicle.VIN = request.VIN;
            vehicle.OdometerKm = request.OdometerKm;
        }
    }
}
