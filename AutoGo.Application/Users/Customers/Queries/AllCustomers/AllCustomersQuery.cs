using AutoGo.Application.Common.Pagination;
using AutoGo.Application.Users.Customers.Dtos;
using ClinicalManagement.Application.Common.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Application.Users.Customers.Queries.AllCustomers
{
    public class AllCustomersQuery : IRequest<Result<PaginationResult<CustomerDto>>>
    {
        public PageParameters PageParameters { get; set; }
    }
}
