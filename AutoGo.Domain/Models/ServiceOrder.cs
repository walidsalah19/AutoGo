using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Domain.Models
{
    public class ServiceOrder:BaseEntity
    {
        public string ReservationId { get; set; }
        public Reservation Reservation { get; set; }

        public DateTime ServiceStartDate { get; set; }
        public DateTime? ServiceEndDate { get; set; }

        public string WorkDescription { get; set; }
        public decimal PartsCost { get; set; }
        public decimal LaborCost { get; set; }
        public decimal TotalCost => PartsCost + LaborCost;

        public string Status { get; set; }   // InProgress, Completed, Cancelled

        public ICollection<Part> PartsUsed { get; set; } = new List<Part>();
    }
}
