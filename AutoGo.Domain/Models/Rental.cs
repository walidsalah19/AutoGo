using AutoGo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Domain.Models
{
    public class Rental : BaseEntity
    {
        public string VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public string customerId { get; set; }   // Customer
        public Customer Customer { get; set; }

        public string DealerId  { get; set; }   // car owner
        public Dealer Dealer { get; set; }

        public DateTime StartDate { get; set; }           // تاريخ بداية الإيجار
        public DateTime EndDate { get; set; }             // تاريخ نهاية الإيجار
        public DateTime? ActualReturnDate { get; set; }   // تاريخ إعادة السيارة فعلياً
        public int DurationDays => (EndDate - StartDate).Days;

        public decimal DailyRate { get; set; }            // سعر اليوم الواحد
        public decimal TotalAmount { get; set; }          // المبلغ الكلي
        public decimal? ExtraCharges { get; set; }        // رسوم إضافية (تأخير / مخالفة)
        public decimal? Discount { get; set; }            // خصم (لو فيه عروض)

        public RentalStatus RentalStatus { get; set; } = RentalStatus.Reserved;         // (Active, Completed, Cancelled, Overdue)

        public string PickupLocation { get; set; }        // مكان استلام السيارة
        public string DropoffLocation { get; set; }       // مكان تسليم السيارة

        public bool IsPaid { get; set; }

        public string Notes { get; set; }                 // ملاحظات إضافية
    }
}
