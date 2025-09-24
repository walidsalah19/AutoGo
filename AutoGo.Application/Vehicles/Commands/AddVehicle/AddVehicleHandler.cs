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

namespace AutoGo.Application.Vehicles.Commands.AddVehicle
{
    public class AddVehicleHandler : IRequestHandler<AddVehicleCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AddVehicleHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IVehicleCacheService _cacheService;
        private readonly IMapper _mapper;


        public AddVehicleHandler(IUnitOfWork unitOfWork, ILogger<AddVehicleHandler> logger, IMediator mediator, IVehicleCacheService cacheService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mediator = mediator;
            _cacheService = cacheService;
            _mapper = mapper;
        }

        public async Task<Result<string>> Handle(AddVehicleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var vehicle = _mapper.Map<Vehicle>(request);
                var vehicleDto = _mapper.Map<VehicleDto>(request);
                vehicleDto.Id = vehicle.Id;
                vehicleDto.Status = vehicle.Status.ToString();
                await _unitOfWork.Repository<Vehicle>().AddAsync(vehicle);
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
    }
}
