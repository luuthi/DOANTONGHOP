using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Bussiness.ViewModel
{
    public class AccountViewModel :BaseViewModel
    {
        [DisplayName("Tên đăng nhập")]
        [Required(ErrorMessage = "Nhập tên đăng nhập")]
        public string UserName { get; set; }
        
        [DisplayName("Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu")]
        public string Password { get; set; }

        [DisplayName("Họ và tên")]
        [Required(ErrorMessage = "Nhập họ tên đầy đủ")]
        public string FullName { get; set; }

        [DisplayName("Giới tính")]
        public bool Gender { get; set; }

        public string Gender_string { get; set; }
        
        [DisplayName("Ngày sinh")]
        public DateTime Birthday { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Số điện thoại")]
        public string PhoneNumber { get; set; }

        [DisplayName("Ảnh đại diện")]
        public string Image { get; set; }
        [DisplayName("Chọn file *")]
        public HttpPostedFileBase filePosted { get; set; }

        [DisplayName("Địa chỉ")]
        public string Address { get; set; }

        [DisplayName("Loại tài khoản")]
        public bool TypeAccount { get; set; }

        public string type_string { get; set; }

        [DisplayName("Tình trạng")]
        public bool Status { get; set; }

        public string status_string { get; set; }
        [DisplayName("Nhóm người dùng")]
        public string GroupAccId { get; set; }
        public string GroupName { get; set; }
    }
}
