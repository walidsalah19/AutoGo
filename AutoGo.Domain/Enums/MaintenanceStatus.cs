using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Domain.Enums
{
    public enum MaintenanceStatus
    {
        Reported,
        InProgress,
        AwaitingParts,
        Completed,
        Cancelled
    }
}
