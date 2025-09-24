using AutoGo.Application.Common.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGo.Application.Abstractions.Cashing;
using AutoGo.Domain.Interfaces.UnitofWork;
using AutoGo.Domain.Models;
using MediatR;

namespace AutoGo.Application.Common.Events.VehicleEvent.CreateVehicle
{
    public class VehicleCreatedEventHandler :INotificationHandler<VehicleCreatedEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDealerCashing _dealerCashing;


        public VehicleCreatedEventHandler(IUnitOfWork unitOfWork, IDealerCashing dealerCashing)
        {
            _unitOfWork = unitOfWork;
            _dealerCashing = dealerCashing;
        }

        public async Task Handle(VehicleCreatedEvent notification, CancellationToken cancellationToken)
        {
            var dealer = await _unitOfWork.Repository<Dealer>().FindEntityById(notification.DealerId);
            if (dealer != null)
            {
                dealer.TotalVehicles += 1;
                await _unitOfWork.CompleteAsync();
                await _dealerCashing.UpdateFieldAsync(notification.DealerId, "TotalVehicles", dealer.TotalVehicles.ToString());
            }
        }
    }
}
