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
        public string UserName { get; set; }

        [DisplayName("Mật khẩu")]
        [Required(ErrorMessage = "Nhập mật khẩu")]
        public string Password { get; set; }

        public bool DuyTriDangNhap { get; set; }
    }
}
