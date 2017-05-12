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
    public class CompanyInfoViewModel : BaseViewModel
    {
        [DisplayName("Tên công ty * :")]
        [Required(ErrorMessage = "Nhập tên công ty")]
        public string CompanyName { get; set; }

        [DisplayName("Email *")]
        [Required(ErrorMessage = "Nhập email của công ty *")]
        public string Email { get; set; }

        [DisplayName("Số điện thoại *")]
        [Required(ErrorMessage = "Nhập số điện thoại công ty")]
        public string Phone { get; set; }

        [DisplayName("Fax :")]
        public string Fax { get; set; }

        [DisplayName("Số di động")]
        public string Mobile { get; set; }

        [DisplayName("Địa chỉ công ty *")]
        [Required(ErrorMessage = "Nhập địa chỉ công ty")]
        public string Address { get; set; }

        [DisplayName("Trang web")]
        public string Website { get; set; }

        [DisplayName("Logo công ty")]
        public string Logo { get; set; }

        [DisplayName("Facebook công ty")]
        public string Facebook { get; set; }
        public HttpPostedFileBase filePosted { get; set; }
    }
}
