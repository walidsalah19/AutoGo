using AutoGo.Api.Extentions;
using AutoGo.Application.Vehicles.Commands.AddVehicle;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoGo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VehicleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddingVehicle([FromBody] AddVehicleCommand vehicleCommand)
        {
            var res = await _mediator.Send(vehicleCommand);
            return this.HandleResult(res);
        }
    }
}
