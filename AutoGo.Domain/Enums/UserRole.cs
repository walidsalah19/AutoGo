using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Domain.Enums
{
    public enum UserRole
    {
        Admin = 1,        // Full system control
        Dealer = 2,       // Car owner / car dealer (adds cars for rent)
        Customer = 3,     // Regular user who rents cars or books services
        WorkshopOwner = 4,// Manages a workshop and its reservations
    }
}
