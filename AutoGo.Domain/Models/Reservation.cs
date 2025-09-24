using AutoGo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Domain.Models
{
    public class Reservation : BaseEntity
    {
        public string WorkshopId { get; set; }
        public Workshop Workshop { get; set; }

        public string UserId { get; set; }   // صاحب السيارة
        public ApplicationUser User { get; set; }

        public string VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }


        public string ReservationNumber { get; set; } 

        public DateTime ReservationDate { get; set; }     // وقت إنشاء الحجز
        public DateTime AppointmentDate { get; set; }     // ميعاد الصيانة / الكشف

        public string ServiceType { get; set; }           // نوع الخدمة (صيانة دورية، كشف، عطل...)
        public string Notes { get; set; }                 // ملاحظات من العميل

        public ReservationsStatus ReservationStatus { get; set; } = ReservationsStatus.Pending;  // Pending, Confirmed, Cancelled, Completed

        public decimal? EstimatedCost { get; set; }       // التكلفة التقديرية لو معروفة
        public bool IsPaid { get; set; }                  // هل تم الدفع المسبق
    }
}
