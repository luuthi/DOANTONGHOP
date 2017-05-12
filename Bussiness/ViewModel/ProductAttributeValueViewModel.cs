using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Bussiness.ViewModel
{
    public class ProductAttributeValueViewModel :BaseViewModel
    {
        public Guid ProductId { get; set; }
        public Guid AttributeId { get; set; }
        public List<SelectListItem> lstAttribute { get; set; }
        public string AttributeName { get; set; }
        public string Value { get; set; }
    }
}
