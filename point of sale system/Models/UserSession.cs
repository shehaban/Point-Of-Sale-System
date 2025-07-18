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
    }
}
