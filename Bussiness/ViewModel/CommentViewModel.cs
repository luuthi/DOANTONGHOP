using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.ViewModel
{
    public class CommentViewModel:BaseViewModel
    {
        [DisplayName("Nội dung")]
        public string Contents { get; set; }
        [DisplayName("Email")]
        public string EmailComment { get; set; }
        [DisplayName("Bình luận cha")]
        public Guid ParrentId { get; set; }
        [DisplayName("Tình trạng")]
        public int Status { get; set; }

        public string Status_str { get; set; }

        public Guid ReferenceId { get; set; }
        public string Reference_str { get; set; }
        
        [DisplayName("Ngày tạo")]
        public DateTime CreatedDate { get; set; }
        [DisplayName("Ngày sửa")]
        public DateTime ModifyDate { get; set; }
    }
}
