using AutoGo.Api.Extentions;
using AutoGo.Application.Common.Pagination;
using AutoGo.Application.Vehicles.Commands.AddVehicle;
using AutoGo.Application.Vehicles.Commands.DeleteVehicle;
using AutoGo.Application.Vehicles.Queries.AllVehicles;
using AutoGo.Application.Vehicles.Queries.NearbyVehicles;
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
        [HttpGet("AllVehicles")]
        public async Task<IActionResult> AllVehicles([FromQuery] PageParameters pageParameters)
        {
            var res = await _mediator.Send(new GetAllVehicles{PageParameters = pageParameters});
            return this.HandleResult(res);
        }
        [HttpGet("Nearby")]
        public async Task<IActionResult> NearbyVehicles([FromQuery] NearbyVehiclesQuery nearbyVehicles)
        {
            var res = await _mediator.Send(nearbyVehicles);
            return this.HandleResult(res);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteVehicle([FromQuery]string VehicleId)
        {
            var res = await _mediator.Send(new DeleteVehicleCommand{VehicleId = VehicleId});
            return this.HandleResult(res);
        }
    }
}
