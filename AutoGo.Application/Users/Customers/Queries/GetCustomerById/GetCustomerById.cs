using AutoGo.Application.Common.Result;
using AutoGo.Application.Users.Customers.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Application.Users.Customers.Queries.GetCustomerById
{
    public class GetCustomerById :IRequest<Result<CustomerDto>>
    {
        public required string customerId { get; set; }
    }
}
