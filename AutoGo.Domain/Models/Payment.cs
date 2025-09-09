using AutoGo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Domain.Models
{
    public class Payment: BaseEntity
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string? RentalId { get; set; }
        public Rental Rental { get; set; }

        public string? InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public string PaymentMethod { get; set; }    // Cash, CreditCard, BankTransfer, Wallet
        public string PaymentStatus { get; set; }    // Pending, Completed, Failed, Refunded
    }
}
