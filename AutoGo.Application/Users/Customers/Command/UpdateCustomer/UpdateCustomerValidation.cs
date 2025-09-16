using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Application.Users.Customers.Command.UpdateCustomer
{
    public class UpdateCustomerValidation : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerValidation()
        {
            RuleFor(x => x.customerId)
        .NotEmpty().WithMessage("Full Id is required");
            RuleFor(x => x.FullName)
         .NotEmpty().WithMessage("Full Name is required")
         .MaximumLength(100).WithMessage("Full Name cannot exceed 100 characters");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required")
                .MaximumLength(250).WithMessage("Address cannot exceed 250 characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email must be a valid email address");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^\+?[0-9]{11}$").WithMessage("Phone number must be valid (11 digits)");

            RuleFor(x => x.City)
                .MaximumLength(50).WithMessage("City cannot exceed 50 characters");

            RuleFor(x => x.Country)
                .MaximumLength(50).WithMessage("Country cannot exceed 50 characters");

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateTime.Today).WithMessage("Date of birth must be in the past");
        }
    }
}
