using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace point_of_sale_system.Models
{
    internal class Inventory
    {
        public int InventoryId { get; set; }
        public int ProductId { get; set; }
        public int ReorderLevel { get; set; }
    }
}
