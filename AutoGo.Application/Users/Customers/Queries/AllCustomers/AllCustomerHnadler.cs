using AutoGo.Application.Common.Context;
using AutoGo.Application.Common.Pagination;
using AutoGo.Application.Users.Customers.Dtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using AutoGo.Application.Common.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Application.Users.Customers.Queries.AllCustomers
{
    public class AllCustomerHnadler : IRequestHandler<AllCustomersQuery, Result<PaginationResult<CustomerDto>>>
    {
        private readonly IAppDbContext appDbContext;
        private readonly IMapper mapper;

        public AllCustomerHnadler(IAppDbContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }

        public async Task<Result<PaginationResult<CustomerDto>>> Handle(AllCustomersQuery request, CancellationToken cancellationToken)
        {
            var query = appDbContext.Customers
                .AsNoTracking()
                .Include(x => x.user)
                .ProjectTo<CustomerDto>(mapper.ConfigurationProvider);

            var customers = await query.ToPagedResultAsync(request.PageParameters.PageNumber, request.PageParameters.Pagesize);
            //var res = mapper.Map<List<CustomerDto>>(customers);
            return Result<PaginationResult<CustomerDto>>.Success(customers);
        }
    }
}
