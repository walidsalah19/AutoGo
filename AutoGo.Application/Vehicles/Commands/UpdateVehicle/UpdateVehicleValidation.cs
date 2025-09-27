using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace AutoGo.Application.Vehicles.Commands.UpdateVehicle
{
    public class UpdateVehicleValidation :AbstractValidator<UpdateVehicleCommand>
    {
        public  UpdateVehicleValidation()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Id is required.");

            RuleFor(v => v.LicensePlate)
                .NotEmpty().WithMessage("License plate is required.")
                .MaximumLength(10).WithMessage("License plate cannot exceed 10 characters.");

            RuleFor(v => v.Make)
                .NotEmpty().WithMessage("Make is required.")
                .MaximumLength(50).WithMessage("Make cannot exceed 50 characters.");

            RuleFor(v => v.Model)
                .NotEmpty().WithMessage("Model is required.")
                .MaximumLength(50).WithMessage("Model cannot exceed 50 characters.");

            RuleFor(v => v.Year)
                .InclusiveBetween(1900, DateTime.UtcNow.Year)
                .WithMessage($"Year must be between 1900 and {DateTime.UtcNow.Year}.");

            RuleFor(v => v.Color)
                .NotEmpty().WithMessage("Color is required.")
                .MaximumLength(30).WithMessage("Color cannot exceed 30 characters.");

            RuleFor(v => v.VIN)
                .NotEmpty().WithMessage("VIN is required.")
                .Matches("^[A-HJ-NPR-Z0-9]{17}$")
                .WithMessage("VIN must be 17 characters and contain only valid characters.");

            RuleFor(v => v.OdometerKm)
                .GreaterThanOrEqualTo(0).WithMessage("Odometer must be greater than or equal to 0.");

            RuleFor(v => v.DailyRate)
                .GreaterThan(0).WithMessage("Daily rate must be greater than 0.");

            RuleFor(v => v.Category)
                .NotEmpty().WithMessage("Category is required.")
                .MaximumLength(30).WithMessage("Category cannot exceed 30 characters.");
        }
    }
}
