using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Application.Users.Dealer.Dtos
{
    public class DealerDto
    {
        public string UserName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ShowroomName { get; set; }         // اسم المعرض
        public string Location { get; set; }             // العنوان
        public string WebsiteUrl { get; set; }           // موقع إلكتروني
        public string Description { get; set; }          // وصف قصير عن المعرض
        public string TaxNumber { get; set; }
        public string LicenseNumber { get; set; }   // ترخيص مزاولة النشاط

        public int EstablishedYear { get; set; }         // سنة تأسيس المعرض
        public int TotalVehicles { get; set; }           // عدد العربيات الموجودة (ممكن تحديثه أو حسابه ديناميكياً)

        public bool IsActive { get; set; } = true;


    }
}
