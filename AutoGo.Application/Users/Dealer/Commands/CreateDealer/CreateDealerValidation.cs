using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace AutoGo.Application.Users.Dealer.Commands.CreateDealer
{
    public class CreateDealerValidation :AbstractValidator<CreateDealerCommand>
    {
        public  CreateDealerValidation()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName is required.")
                .MaximumLength(100).WithMessage("UserName must be less than 100 characters.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(200);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Passowrd)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches(@"[0-9]").WithMessage("Password must contain at least one digit")
                .Matches(@"[\@\!\?\*\#]").WithMessage("Password must contain at least one special character (@!?*#)");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^\+?[0-9]{11}$").WithMessage("Phone number must be valid (11 digits)");

            RuleFor(x => x.ShowroomName)
                .NotEmpty().WithMessage("Showroom name is required.")
                .MaximumLength(150);

            RuleFor(x => x.Longitude)
                .NotNull().WithMessage("Longitude is required.");
            RuleFor(x => x.Latitude)
                .NotNull().WithMessage("Latitude is required.");

            RuleFor(x => x.WebsiteUrl)
                .Must(url => string.IsNullOrEmpty(url) || Uri.TryCreate(url, UriKind.Absolute, out _))
                .WithMessage("Website URL must be a valid URL.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must be less than 500 characters.");

            RuleFor(x => x.TaxNumber)
                .NotEmpty().WithMessage("Tax number is required.")
                .Matches(@"^\d{10,15}$").WithMessage("Tax number must be numeric.");

            RuleFor(x => x.LicenseNumber)
                .NotEmpty().WithMessage("License number is required.")
                .MaximumLength(50);

            RuleFor(x => x.EstablishedYear)
                .InclusiveBetween(1950, DateTime.Now.Year)
                .WithMessage($"Established year must be between 1900 and {DateTime.Now.Year}.");
        }
    }
}
