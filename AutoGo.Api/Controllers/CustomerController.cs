using AutoGo.Api.Extentions;
using AutoGo.Application.Common.Pagination;
using AutoGo.Application.Users.Customers.Command.CreateCustomer;
using AutoGo.Application.Users.Customers.Queries.AllCustomers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AutoGo.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator mediator;

        public CustomerController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand customer)
        {
            var res = await mediator.Send(customer);
            return this.HandleResult(res);
        }
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<IActionResult> AllCutomers([FromQuery]PageParameters pageParameters)
        {
            var res = await mediator.Send(new AllCustomersQuery { PageParameters=pageParameters});
            return this.HandleResult(res);
        }
    }
}
