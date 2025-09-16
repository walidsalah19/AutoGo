using AutoGo.Application.Common.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AutoGo.Application.Users.Customers.Command.UpdateCustomer
{
    public class UpdateCustomerCommand : IRequest<Result<string>>
    {
        public required string customerId { get; set; }

        public required string FullName { get; set; }
        public required string Address { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }
        public required DateTime DateOfBirth { get; set; }
    }
}
