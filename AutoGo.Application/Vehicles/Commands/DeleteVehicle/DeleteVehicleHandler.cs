using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGo.Application.Abstractions.Cashing;
using AutoGo.Application.Abstractions.Cloudinary;
using AutoGo.Application.Common.Events.VehicleEvent.DeleteVehicle;
using AutoGo.Application.Common.Result;
using AutoGo.Domain.Enums;
using AutoGo.Domain.Interfaces.UnitofWork;
using AutoGo.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AutoGo.Application.Vehicles.Commands.DeleteVehicle
{
    public class DeleteVehicleHandler : IRequestHandler<DeleteVehicleCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly IVehicleCacheService _cacheService;
        private readonly ILogger<DeleteVehicleHandler> _logger;

        public DeleteVehicleHandler(IUnitOfWork unitOfWork, IMediator mediator, IVehicleCacheService cacheService, ILogger<DeleteVehicleHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _cacheService = cacheService;
            _logger = logger;
        }

        public async Task<Result<string>> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var vehicle = await _unitOfWork.Repository<Vehicle>().FindEntityById(request.VehicleId);
                if (vehicle == null)
                    return Result<string>.Failure(new Error(message:"this vehicle not exist ",code:(int)ErrorCodes.NotFound));
                await _unitOfWork.Repository<Vehicle>().Remove(vehicle);
                await _unitOfWork.CompleteAsync();
                await _cacheService.DeleteVehicleAsync(request.VehicleId);
                await _mediator.Publish(new VehicleDeletedEvent(VehicleId: vehicle.Id.ToString(), DealerId:vehicle.DealerId));
                return Result<string>.Success( "delete vehicle Successfully");

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}
