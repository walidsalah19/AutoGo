using AutoGo.Application.Abstractions.IdentityServices;
using AutoGo.Domain.Enums;
using AutoGo.Domain.Interfaces.UnitofWork;
using AutoGo.Domain.Models;
using AutoMapper;
using AutoGo.Application.Common.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGo.Application.Common.Result;

namespace AutoGo.Application.Users.Customers.Command.CreateCustomer
{
	public class CreateCustomerhandler : IRequestHandler<CreateCustomerCommand, Result<string>>
	{
		private readonly IUsersServices _UserServices;
		private readonly IMapper mapper;
		private readonly IUnitOfWork unitOfWork;

		public CreateCustomerhandler(IUsersServices userServices, IMapper mapper, IUnitOfWork unitOfWork)
		{
			_UserServices = userServices;
			this.mapper = mapper;
			this.unitOfWork = unitOfWork;
		}

		public async Task<Result<string>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
		{
			var applicationUser = mapper.Map<ApplicationUser>(request);
			var customer = mapper.Map<Customer>(request);

			try
			{
				var res = await _UserServices.CreateAsync(applicationUser, UserRole.Customer.ToString(), request.Passowrd);
				if (!res.isSuccessed)
				{
					return res; // لو إنشاء اليوزر فشل، وقف العملية هنا
				}

				customer.userId = applicationUser.Id;
				await unitOfWork.Repository<Customer>().AddAsync(customer);
				await unitOfWork.CompleteAsync();
				return Result<string>.Success(" customer created successfully");
			}
			catch (Exception ex)
			{
				// ✅ ممكن هنا تضيف Rollback لليوزر لو كان اتسجل بالفعل
				return Result<string>.Failure(new Error(message: ex.Message, code:(int) ErrorCodes.BadRequest));
			}

		}
	}
}
