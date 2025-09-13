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
        public int InvoiceNumber { get; set; }

        public string customerId { get; set; }//customer
        public Customer customer { get; set; }

        public Guid? RentalId { get; set; }
        public Rental Rental { get; set; }

        public Guid? ServiceOrderId { get; set; }
        public ServiceOrder ServiceOrder { get; set; }
        public Guid? paymentId { get; set; }
        public Payment Payment { get; set; }
        public DateTime IssueDate { get; set; } = DateTime.UtcNow;
        public DateTime DueDate { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal RemainingAmount => TotalAmount - PaidAmount;

        public string Status { get; set; }   // Paid, PartiallyPaid, Unpaid, Cancelled



    }
}
