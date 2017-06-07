using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;

namespace Bussiness.ViewModel
{
    public class ProductViewModel:BaseViewModel
    {
        [DisplayName("Nhóm sản phẩm")]
        [Required(ErrorMessage = "Nhập nhóm sản phẩm")]
        public Guid GroupId { get; set; }
        public string GroupName { get; set; }
        public List<SelectListItem> lstGroup { get; set; }

        [DisplayName("Tên sản phẩm")]
        [Required(ErrorMessage = "Nhập tên sản phẩm")]
        public string  ProductName { get; set; }

        [DisplayName("Giá nhập")]
        public decimal Price { get; set; }

        public string Price_Format { get; set; }

        [DisplayName("Đơn vị tính")]
        [Required(ErrorMessage = "Nhập đơn vị tính")]
        public string Unit { get; set; }

        [DisplayName("Ảnh đại diện")]
        public string Image { get; set; }
        public HttpPostedFileBase filePosted { get; set; }

        [DisplayName("Danh mục sản phẩm")]
        public string Category { get; set; }
        public string[] EnumCategory { get; set; }
        public List<SelectListItem> lstCategory { get; set; }

        [DisplayName("Tình trạng")]
        public bool Status { get; set; }

        [DisplayName("Có thể bình luận")]
        public bool CanComment { get; set; }

        [DisplayName("Mô tả ngắn")]
        public string Description { get; set; }

        [DisplayName("Thông tin chi tiết")]
        public string  DetailInfo { get; set; }

        [DisplayName("Nhà cung cấp")]
        public string Vendor { get; set; }

        [DisplayName("Album ảnh")]
        public string Gallery { get; set; }

        [DisplayName("Người tạo")]
        public string Creator { get; set; }
        [DisplayName("Ngày tạo")]
        public DateTime CreatedDate { get; set; }
        [DisplayName("Người sửa")]
        public string Modifier { get; set; }
        [DisplayName("Ngày sửa")]
        public DateTime ModifyDate { get; set; }
    }
}
