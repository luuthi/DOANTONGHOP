using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Bussiness.ViewModel
{
    public class MenuViewModel :BaseViewModel
    {
        [DisplayName("Tiêu đề")]
        public string Title { get; set; }

        [DisplayName("Thứ tự")]
        public int Order { get; set; }

        [DisplayName("Menu cha")]
        public Guid ParrentId { get; set; }

        public List<SelectListItem> lstParentMenu { get; set; }
        public string Title_Parent { get; set; }

        [DisplayName("Tình trạng")]
        public bool Status { get; set; }

        [DisplayName("Link")]
        public string Link { get; set; }
    }
}
