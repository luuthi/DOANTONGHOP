using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.ViewModel
{
    public class HomeViewModel
    {
        public List<ProductViewModel> lstTopSellProduct { get; set; }
        public List<ProductViewModel> lstNewProduct { get; set; }
        public List<ProductViewModel> lstOutstandingProduct { get; set; }
        public List<NewsViewModel> lstNews { get; set; }
    }
}
