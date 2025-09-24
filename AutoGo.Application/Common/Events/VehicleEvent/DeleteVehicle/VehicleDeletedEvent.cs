using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Application.Common.Events.VehicleEvent.DeleteVehicle
{
    public record VehicleDeletedEvent(string VehicleId, string DealerId) : INotification;

}
