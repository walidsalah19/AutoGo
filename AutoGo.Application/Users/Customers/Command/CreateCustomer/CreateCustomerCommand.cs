using AutoGo.Application.Common.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Application.Users.Customers.Command.CreateCustomer
{
    public class CreateCustomerCommand:IRequest<Result<string>>
    {

        public string FullName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Passowrd { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsActive { get; set; }

    }
}
