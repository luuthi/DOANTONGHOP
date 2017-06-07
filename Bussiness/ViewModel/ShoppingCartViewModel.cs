using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.ViewModel
{
    public class ShoppingCartViewModel
    {
        public string ProductName { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public decimal Price { get; set; }
        public decimal SubTotal { get; set; }
        public string Image { get; set; }
    }
}
