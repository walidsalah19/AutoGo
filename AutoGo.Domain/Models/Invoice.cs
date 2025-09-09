using AutoGo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Domain.Models
{
    public class Invoice : BaseEntity
    {
        public string InvoiceNumber { get; set; } = Guid.NewGuid().ToString().Substring(0, 8);

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string? RentalId { get; set; }
        public Rental Rental { get; set; }

        public string? ServiceOrderId { get; set; }
        public ServiceOrder ServiceOrder { get; set; }

        public DateTime IssueDate { get; set; } = DateTime.UtcNow;
        public DateTime DueDate { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal RemainingAmount => TotalAmount - PaidAmount;

        public string Status { get; set; }   // Paid, PartiallyPaid, Unpaid, Cancelled
    }
}
