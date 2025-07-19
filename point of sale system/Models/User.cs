using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace point_of_sale_system.Models
{
    internal class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public int FailedAttempts { get; set; }
        public DateTime? LastAttempt { get; set; }
        public bool IsLocked { get; set; }
        public bool IsDeleted { get; set; } // Add this line

    }
}
