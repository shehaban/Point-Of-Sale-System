using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace point_of_sale_system.Models
{
    internal class UserSession
    {
        public static string CurrentUserRole { get; set; }
        public static string CurrentUsername { get; set; }

        public static bool IsAdmin()
        {
            return CurrentUserRole?.Equals("Admin", StringComparison.OrdinalIgnoreCase) ?? false;
        }

        public static void Login(User user)
        {
            CurrentUserRole = user.Role;
            CurrentUsername = user.Username;
        }

        public static void Logout()
        {
            CurrentUserRole = null;
            CurrentUsername = null;
        }
    }
}
