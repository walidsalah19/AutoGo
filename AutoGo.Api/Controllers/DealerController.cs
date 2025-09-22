using AutoGo.Api.Extentions;
using AutoGo.Application.Users.Dealer.Commands.CreateDealer;
using AutoGo.Application.Users.DeleteCustomer;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoGo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DealerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDealer([FromBody] CreateDealerCommand command)
        {
            var res = await _mediator.Send(command);
            return this.HandleResult(res);

        }
    }
}
