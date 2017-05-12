using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using NSX_Common;

namespace Bussiness.ViewModel
{
    public class NewsViewModel : BaseViewModel
    {
        [DisplayName("Tiêu đề")]
        [Required(ErrorMessage = "Nhập tiêu đề")]
        public string Title { get; set; }

        [DisplayName("Mô tả")]
        [Required(ErrorMessage = "Nhập mô tả tin tức")]
        public string Description { get; set; }

        [DisplayName("Nội dung")]
        public string Content { get; set; }

        [DisplayName("Ảnh")]
        public string Image { get; set; }
        [DisplayName("Chọn file *")]
        public HttpPostedFileBase filePosted { get; set; }

        [DisplayName("Thứ tự tin tức")]
        [Required(ErrorMessage = "Nhập số thứ tự")]
        public int Order { get; set; }

        [DisplayName("Loại tin tức")]
        public string Category { get; set; }
        public int[] EnumCategory { get; set; }

        [DisplayName("Có thể bình luận")]
        public bool CanComment { get; set; }

        [DisplayName("Tình trạng")]
        public bool Status { get; set; }

        [DisplayName("Nhóm tin tức")]
        [Required(ErrorMessage = "Nhập nhóm tin tức")]
        public Guid GroupId { get; set; }

        public string NameGroup { get; set; }
        public List<NewsGroupViewModel> lstNewsGroup { get; set; }

        [DisplayName("Người tạo")]
        public string PostAccount { get; set; }
        [DisplayName("Ngày tạo")]
        public DateTime PostedDate { get; set; }
        [DisplayName("Người sửa")]
        public string Modifier { get; set; }
        [DisplayName("Ngày sửa")]
        public DateTime ModifyDate { get; set; }
    }
}
