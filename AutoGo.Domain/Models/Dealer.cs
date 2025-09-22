using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Domain.Models
{
   public class Dealer :BaseEntity
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string ShowroomName { get; set; }         // اسم المعرض
        public string? WebsiteUrl { get; set; }           // موقع إلكتروني
        public string Description { get; set; }          // وصف قصير عن المعرض
        public string TaxNumber { get; set; }
        public string LicenseNumber { get; set; }   // ترخيص مزاولة النشاط

        public int EstablishedYear { get; set; }         // سنة تأسيس المعرض
        public int TotalVehicles { get; set; }           // عدد العربيات الموجودة (ممكن تحديثه أو حسابه ديناميكياً)
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        // Navigation
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
