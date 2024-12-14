using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoyNuget.Core.Models
{
    public class Role
    {
        public int Id { get; set; } // کلید اصلی
        public string Name { get; set; } = string.Empty; // نام نقش (مثلاً Admin, User)
        public ICollection<User> Users { get; set; } = new List<User>(); // کاربران مرتبط با این نقش
    }

}
