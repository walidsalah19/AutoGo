using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGo.Application.Common.Result;
using MediatR;

namespace AutoGo.Application.Vehicles.Commands.DeleteVehicle
{
    public class DeleteVehicleCommand :IRequest<Result<string>>
    {
        public required string VehicleId { get; set; }
    }
}
