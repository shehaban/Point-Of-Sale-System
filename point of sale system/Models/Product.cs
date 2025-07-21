using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace point_of_sale_system.Models
{
    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public decimal unit_price { get; set; }
        public decimal purchase_price { get; set; }
        public int quantity { get; set; }
        public bool IsDeleted { get; set; } 
    }
}
