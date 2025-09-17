using AutoGo.Api.Extentions;
using AutoGo.Application.Authintication.ChangePassword;
using AutoGo.Application.Authintication.Login;
using AutoGo.Application.Authintication.Logout;
using AutoGo.Application.Authintication.RefreshToken;
using AutoGo.Application.Users.ActivationUsers.Activation;
using AutoGo.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AutoGo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator mediator;

        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginComand loginComand)
        {
            var res = await mediator.Send(loginComand);
            return this.HandleResult(res);
        }
        [Authorize]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand commad)
        {
            var res = await mediator.Send(commad);
            return this.HandleResult(res);
        }
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefrashTokenCommad commad)
        {
            var res = await mediator.Send(commad);
            return this.HandleResult(res);
        }
        [Authorize]
        [HttpGet("Logout")]
        public async Task<IActionResult> Logout([FromQuery] string userId)
        {
            var res = await mediator.Send(new LogoutCommand { userId = userId });
            return this.HandleResult(res);
        }
       
    }
}
