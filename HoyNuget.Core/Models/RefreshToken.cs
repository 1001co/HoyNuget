using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoyNuget.Core.Models
{
    public class RefreshToken
    {
        public int Id { get; set; } // کلید اصلی
        public string Token { get; set; } = string.Empty; // مقدار توکن
        public DateTime ExpiryDate { get; set; } // تاریخ انقضا
        public int UserId { get; set; } // کلید خارجی به User
        public User? User { get; set; }  // رابطه با User
    }

}
