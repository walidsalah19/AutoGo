using AutoGo.Api.Extentions;
using AutoGo.Application.Users.ActivationUsers.Activation;
using AutoGo.Application.Users.ActivationUsers.SpecificPeriod;
using AutoGo.Application.Users.DeleteCustomer;
using AutoGo.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoGo.Api.Controllers
{
    [Authorize(Roles = nameof(UserRole.Admin))]
    [Route("api/[controller]")]
    [ApiController]
    public class ManageUsersController : ControllerBase
    {
        private readonly IMediator mediator;

        public ManageUsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPut("ManageUserActivation")]
        public async Task<IActionResult> Activation([FromBody] ChangeActivationCommand command)
        {
            var res = await mediator.Send(command);
            return this.HandleResult(res);
        }
        [HttpPut("DeactivateForSpecificPeriod")]
        public async Task<IActionResult> DeactivateForSpecificPeriod([FromBody] DeactivateForSpecificPeriod command)
        {
            var res = await mediator.Send(command);
            return this.HandleResult(res);
        }
        [Authorize]
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteCustomer([FromQuery] string UserId)
        {
            var res = await mediator.Send(new DeleteUserCommand() { UserId = UserId });
            return this.HandleResult(res);
        }
    }
}
