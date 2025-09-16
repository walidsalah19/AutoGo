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

namespace AutoGo.Application.Users.Customers.Command.UpdateCustomer
{
    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, Result<string>>
    {
        private readonly IUsersServices _UserServices;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public UpdateCustomerHandler(IUsersServices userServices, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _UserServices = userServices;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _UserServices.UpdateAsync(request);
                if (!res.isSuccessed)
                {
                    return res; // لو إنشاء اليوزر فشل، وقف العملية هنا
                }
                var r = 0;
                var t = 200 /r ;
                var customer = mapper.Map<Customer>(request);
                //var customer = await unitOfWork.Repository<Customer>().GetEntityById(request.customerId);
                customer.DateOfBirth = request.DateOfBirth;
                customer.City = request.City;
                customer.Country = request.Country;
                await unitOfWork.Repository<Customer>().UpdateAsync(customer);
                await unitOfWork.CompleteAsync();
                return Result<string>.Success(" customer Updated successfully");
            }
            catch (Exception ex)
            {
                // ✅ ممكن هنا تضيف Rollback لليوزر لو كان اتسجل بالفعل
               // return Result<string>.Failure(new Error(message: ex.Message, code: (int)ErrorCodes.BadRequest));
                throw;
            }
        }
    }
}
