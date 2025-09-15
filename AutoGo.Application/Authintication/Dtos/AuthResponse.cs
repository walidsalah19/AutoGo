using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Application.Authintication.Dtos
{
    public class AuthResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiresAt { get; set; }
        public int ExpiresIn { get; set; }
        public string userName { get; set; }
        public string userId { get; set; }

    }
}
