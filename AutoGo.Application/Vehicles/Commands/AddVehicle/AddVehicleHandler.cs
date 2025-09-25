using AutoGo.Application.Abstractions.Cashing;
using AutoGo.Application.Common.Events.VehicleEvent.CreateVehicle;
using AutoGo.Application.Common.Result;
using AutoGo.Application.Vehicles.Dto;
using AutoGo.Domain.Interfaces.UnitofWork;
using AutoGo.Domain.Models;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGo.Application.Abstractions.Cloudinary;
using Microsoft.AspNetCore.Http;

namespace AutoGo.Application.Vehicles.Commands.AddVehicle
{
    public class AddVehicleHandler : IRequestHandler<AddVehicleCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AddVehicleHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IVehicleCacheService _cacheService;
        private readonly IMapper _mapper;
        private readonly ICloudinaryServices _cloudinaryServices;


        public AddVehicleHandler(IUnitOfWork unitOfWork, ILogger<AddVehicleHandler> logger, IMediator mediator, IVehicleCacheService cacheService, IMapper mapper, ICloudinaryServices cloudinaryServices)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mediator = mediator;
            _cacheService = cacheService;
            _mapper = mapper;
            _cloudinaryServices = cloudinaryServices;
        }

        public async Task<Result<string>> Handle(AddVehicleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var vehicle = _mapper.Map<Vehicle>(request);
                var vehicleDto = _mapper.Map<VehicleDto>(vehicle);
               
                vehicleDto.Images = await SaveVehicleInDb(vehicle,request.Images);


                await _cacheService.CacheVehicleAsync(vehicleDto);
                await _mediator.Publish(new VehicleCreatedEvent
                (
                    DealerId : request.DealerId, 
                    VehicleId : vehicle.Id.ToString()

                ));


                return Result<string>.Success("Adding vehicle successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        private async Task<List<string>> SaveVehicleInDb(Vehicle vehicle,List<IFormFile> images)
        {
            try
            {
                // 1️⃣ أضف السيارة واحفظها عشان Id يتولد
                await _unitOfWork.Repository<Vehicle>().AddAsync(vehicle);
                await _unitOfWork.CompleteAsync();

                // 2️⃣ ارفع الصور على Cloudinary
                var imageUrls = await _cloudinaryServices.AddVehicleImagesAsync(images);

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
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                throw; // أعد رمي الاستثناء عشان الـ Middleware يعالجه
            }
        }
    }
}
