using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Domain.Models
{
   public class Workshop : BaseEntity
    {
        public string Name { get; set; }                  // اسم الورشة
        public string Address { get; set; }               // عنوان الورشة
        public string PhoneNumber { get; set; }           // رقم التواصل
        public string Email { get; set; }                 // إيميل الورشة
        public string WebsiteUrl { get; set; }            // موقع إلكتروني (اختياري)

        public string Description { get; set; }           // وصف قصير عن الورشة
        public int EstablishedYear { get; set; }          // سنة التأسيس
        public int Capacity { get; set; }                 // الطاقة الاستيعابية (عدد العربيات اللي ممكن الورشة تستقبلها في نفس الوقت)
        public string Specialization { get; set; }   // كهرباء، ميكانيكا، فحص شامل

        // Owner (WorkshopOwner)
        public string OwnerId { get; set; }
        public ApplicationUser Owner { get; set; }


        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    }
}
