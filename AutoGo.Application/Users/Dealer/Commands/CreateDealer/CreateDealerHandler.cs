using AutoGo.Application.Abstractions.IdentityServices;
using AutoGo.Application.Common.Result;
using AutoGo.Domain.Enums;
using AutoGo.Domain.Interfaces.UnitofWork;
using AutoGo.Domain.Models;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGo.Application.Abstractions.Cashing;
using AutoGo.Application.Users.Dealer.Dtos;
using Microsoft.Extensions.Logging;

namespace AutoGo.Application.Users.Dealer.Commands.CreateDealer
{
    public class CreateDealerHandler : IRequestHandler<CreateDealerCommand, Result<string>>
    {
        private readonly IUsersServices _UserServices;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<CreateDealerHandler> _logger;
        private readonly IDealerCashing _dealerCashing;


        public CreateDealerHandler(IUsersServices userServices, IMapper mapper, IUnitOfWork unitOfWork, ILogger<CreateDealerHandler> logger, IDealerCashing dealerCashing)
        {
            _UserServices = userServices;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            _logger = logger;
            _dealerCashing = dealerCashing;
        }

        public async Task<Result<string>> Handle(CreateDealerCommand request, CancellationToken cancellationToken)
        {
            var applicationUser = mapper.Map<ApplicationUser>(request);
            var dealer = mapper.Map<Domain.Models.Dealer>(request);
            var dealerDto = mapper.Map<DealerDto>(request);
            try
            {
                var res = await _UserServices.CreateAsync(applicationUser, UserRole.Dealer.ToString(),
                    request.Passowrd);
                if (!res.isSuccessed)
                {
                    return res; // لو إنشاء اليوزر فشل، وقف العملية هنا
                }

                dealer.UserId = applicationUser.Id;
                dealerDto.Id = applicationUser.Id;
                await unitOfWork.Repository<Domain.Models.Dealer>().AddAsync(dealer);
                await unitOfWork.CompleteAsync();
                await _dealerCashing.CacheDealerAsync(dealerDto);
                return Result<string>.Success($" Create Dealer Account is successfully for {applicationUser.UserName}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
