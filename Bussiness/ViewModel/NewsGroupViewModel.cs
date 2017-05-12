using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.ViewModel
{
    public class NewsGroupViewModel :BaseViewModel
    {
        [DisplayName("Tên nhóm tin")]
        [Required(ErrorMessage = "Nhập tên nhóm tin tức")]
        public string NameGroup { get; set; }

        [DisplayName("Mô tả")]
        public string Description { get; set; }

        [DisplayName("Tình trạng")]
        public bool Status { get; set; }


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
