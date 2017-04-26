using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSX_Common;

namespace Bussiness.ViewModel
{
    public class RoleViewModel: BaseViewModel
    {
        [DisplayName("Mã quyền")]
        [Required(ErrorMessage = "Nhập mã quyền")]
        [Range(1, int.MaxValue, ErrorMessage = "Chọn mã quyền")]
        public RightAdmin Code { get; set; }

        [DisplayName("Tên quyền")]
        [Required(ErrorMessage = "Nhập quyền")]
        public string Role { get; set; }
    }
}
