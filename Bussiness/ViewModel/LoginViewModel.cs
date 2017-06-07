using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.ViewModel
{
    public class LoginViewModel
    {
        [DisplayName("Tên đăng nhập")]
        [Required(ErrorMessage = "Nhập tên đăng nhập")]
        [StringLength(32, MinimumLength = 3, ErrorMessage = "Tên đăng nhập trong khoảng 6-32  kí tự")]
        [RegularExpression("^[a-zA-Z0-9]{4,10}$", ErrorMessage = "Tên đăng nhập không chứa kí tự đặc biệt")]
        public string UserName { get; set; }

        [DisplayName("Mật khẩu")]
        [Required(ErrorMessage = "Nhập mật khẩu")]
        [StringLength(32, MinimumLength = 3, ErrorMessage = "Mật khẩu trong khoảng 8-32  kí tự")]
        public string Password { get; set; }

        public bool DuyTriDangNhap { get; set; }
    }
}
