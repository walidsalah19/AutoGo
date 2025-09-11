using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Domain.Models
{
    public class MaintenanceRecord:BaseEntity
    {
        public Guid VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public DateTime MaintenanceDate { get; set; } = DateTime.UtcNow;
        public string Description { get; set; }

        public decimal Cost { get; set; }
        public string PerformedBy { get; set; }   // اسم الورشة أو الفني

        public string? WorkshopId { get; set; }
        public Workshop Workshop { get; set; }

        public ICollection<Part> Parts { get; set; } = new List<Part>();
    }
}
