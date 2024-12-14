using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoyNuget.Core.Models
{
    public class User
    {
        public int Id { get; set; } // کلید اصلی
        public string Username { get; set; } = string.Empty; // نام کاربری
        public string PasswordHash { get; set; } = string.Empty; // هش رمز عبور
        public string Email { get; set; } = string.Empty; // ایمیل کاربر
        public ICollection<Role> Roles { get; set; } = new List<Role>(); // نقش‌های کاربر
    }

}
