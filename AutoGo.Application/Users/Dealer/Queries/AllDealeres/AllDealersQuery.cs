using AutoGo.Application.Common.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGo.Application.Common.Result;
using AutoGo.Application.Users.Dealer.Dtos;
using MediatR;

namespace AutoGo.Application.Users.Dealer.Queries.AllDealeres
{
    public class AllDealersQuery :IRequest<Result<PaginationResult<DealerDto>>>
    {
        public PageParameters PageParameters { get; set; }

    }
}
