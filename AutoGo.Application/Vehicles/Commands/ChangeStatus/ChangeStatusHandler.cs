using AutoGo.Application.Abstractions.Cashing;
using AutoGo.Application.Common.Result;
using AutoGo.Application.Vehicles.Commands.DeleteVehicle;
using AutoGo.Domain.Enums;
using AutoGo.Domain.Interfaces.UnitofWork;
using AutoGo.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Application.Vehicles.Commands.ChangeStatus
{
    public class ChangeStatusHandler : IRequestHandler<ChangeStatusCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVehicleCacheService _cacheService;
        private readonly ILogger<DeleteVehicleHandler> _logger;

        public ChangeStatusHandler(IUnitOfWork unitOfWork, IVehicleCacheService cacheService, ILogger<DeleteVehicleHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
            _logger = logger;
        }

        public async Task<Result<string>> Handle(ChangeStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var vehicle = await _unitOfWork.Repository<Vehicle>().FindEntityById(request.VehicleId);
                if (vehicle==null)
                    return Result<string>.Failure(new Error(message: "this vehicle not exist ", code: (int)ErrorCodes.NotFound));

                vehicle.Status = request.Status;
                await _unitOfWork.CompleteAsync();
                await _cacheService.UpdateFieldAsync(request.VehicleId, "Status", request.Status.ToString());
                return Result<string>.Success("Update vehicle status Successfully");

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}
