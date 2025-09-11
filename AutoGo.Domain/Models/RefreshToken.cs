using AutoGo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalManagement.Domain.Models
{
   public class RefreshToken
    {
        public Guid Id { get; set; }

        public string Token { get; set; }

        public DateTime ExpireOnUtc { get; set; }

        public string UserId { get; set; }
        public ApplicationUser user { get; set; }
    }
}
