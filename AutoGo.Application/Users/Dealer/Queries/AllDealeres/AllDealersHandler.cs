using AutoGo.Application.Common.Context;
using AutoGo.Application.Common.Pagination;
using AutoGo.Application.Common.Result;
using AutoGo.Application.Users.Dealer.Dtos;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AutoGo.Application.Users.Dealer.Queries.AllDealeres
{
    public class AllDealersHandler : IRequestHandler<AllDealersQuery, Result<PaginationResult<DealerDto>>>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper mapper;


        public AllDealersHandler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            this.mapper = mapper;
        }

        public async Task<Result<PaginationResult<DealerDto>>> Handle(AllDealersQuery request, CancellationToken cancellationToken)
        {
            var query = _appDbContext.Dealers
                .AsNoTracking()
                .Include(x => x.User)
                .ProjectTo<DealerDto>(mapper.ConfigurationProvider);

            var dealers =
                await query.ToPagedResultAsync(request.PageParameters.PageNumber, request.PageParameters.Pagesize);
            return Result<PaginationResult<DealerDto>>.Success(dealers);
        }
    }
}
