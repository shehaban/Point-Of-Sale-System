using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace point_of_sale_system.Models
{
    internal class Sale
    {
        public int SaleId { get; set; }
        public int InvoiceId { get; set; }
        public int ProductId { get; set; }
        public int QuantitySold { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
