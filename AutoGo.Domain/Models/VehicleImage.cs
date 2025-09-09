using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Domain.Models
{
    public class VehicleImage : BaseEntity
    {
        public string Url { get; set; }

        public string VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
