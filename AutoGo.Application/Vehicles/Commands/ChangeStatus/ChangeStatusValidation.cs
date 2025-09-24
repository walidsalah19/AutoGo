using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace AutoGo.Application.Vehicles.Commands.ChangeStatus
{
    public class ChangeStatusValidation: AbstractValidator<ChangeStatusCommand>
    {
        public ChangeStatusValidation()
        {

            RuleFor(x => x.VehicleId)
                .NotEmpty()
                .WithMessage("VehicleId must be a valid value.");
            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("Status must be a valid value of VehicleStatus enum.");
        }

    }
}
