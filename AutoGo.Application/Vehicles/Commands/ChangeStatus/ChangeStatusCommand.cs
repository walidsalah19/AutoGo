using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGo.Application.Common.Result;
using AutoGo.Domain.Enums;
using MediatR;

namespace AutoGo.Application.Vehicles.Commands.ChangeStatus
{
    public class ChangeStatusCommand:IRequest<Result<string>>
    {
        public required string VehicleId { get; set; }
        public required VehicleStatus Status { get; set; }
    }
}
