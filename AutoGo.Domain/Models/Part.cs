using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Domain.Models
{
    public class Part:BaseEntity
    {
        public string PartName { get; set; }
        public string PartNumber { get; set; }
        public string Manufacturer { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Guid? MaintenanceRecordId { get; set; }
        public MaintenanceRecord MaintenanceRecord { get; set; }
    }
}
