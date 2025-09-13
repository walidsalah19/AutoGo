using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Domain.Models
{
    public class Customer
    {
        // ✅ بيانات إضافية للعميل
        public string? City { get; set; }
        public string? Country { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public bool IsActive { get; set; } = true;
        public string userId { get; set; }
        public ApplicationUser user { get; set; }

        // ✅ Navigation Properties (علاقات مع جداول أخرى لو عندك)
        public ICollection<Invoice>? Invoices { get; set; }
        public ICollection<Rental> Rentals { get; set; }
    }
}
