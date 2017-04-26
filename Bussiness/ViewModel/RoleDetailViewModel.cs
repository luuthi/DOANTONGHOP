using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.ViewModel
{
    public class RoleDetailViewModel:BaseViewModel
    {
        [DisplayName("Tên quyền")]
        public string Role { get; set; }

        [DisplayName("Tên nhóm người dùng")]
        public string GroupAccId { get; set; }
    }
}
