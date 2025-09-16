using AutoGo.Application.Common.Context;
using AutoGo.Application.Common.Result;
using AutoGo.Application.Users.Customers.Dtos;
using AutoGo.Domain.Enums;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Application.Users.Customers.Queries.GetCustomerById
{
    public class GetCustomerByIdHandler : IRequestHandler<GetCustomerById, Result<CustomerDto>>
    {
        private readonly IAppDbContext appDbContext;
        private readonly IMapper mapper;

        public GetCustomerByIdHandler(IAppDbContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }

        public async Task<Result<CustomerDto>> Handle(GetCustomerById request, CancellationToken cancellationToken)
        {
            var user = await appDbContext.Customers
                .Include(x => x.user)
                .ProjectTo<CustomerDto>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.CustomerId.Equals(request.customerId));
            if (user == null)
            {
                return Result<CustomerDto>.Failure(new Error(message: "User not found", code: (int)ErrorCodes.NotFound));
            }
            return Result<CustomerDto>.Success(user);
        }
    }
}
