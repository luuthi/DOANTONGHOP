using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.ViewModel
{
    public class GroupAccountViewModel :BaseViewModel
    {
        [DisplayName("Mã nhóm người sử dụng")]
        [Required(ErrorMessage = "Nhập mã nhóm")]
        public string GroupCode { get; set; }

        [DisplayName("Tên nhóm người sử dụng")]
        [Required(ErrorMessage = "Nhập tên nhóm")]
        public string GroupName { get; set; }
    }
}
