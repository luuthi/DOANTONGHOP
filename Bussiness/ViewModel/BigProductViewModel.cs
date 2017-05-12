using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.ViewModel
{
    public class BigProductViewModel
    {
        public ProductViewModel ProductViewModel { get; set; }
        public List<ProductAttributeValueViewModel> ProductAttributeValueViewModels { get; set; }
    }
}
