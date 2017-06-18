using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Bussiness.ViewModel
{
    public class OrderViewModel : BaseViewModel
    {
        [DisplayName("Mã đơn hàng")]
        public string OrderCode { get; set; }

        [DisplayName("Tài khoản đặt")]
        public string OrderAccount { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public DateTime OrderDate { get; set; }

        [DisplayName("Tình trạng")]
        public int Status { get; set; }
        public string Status_str { get; set; }

        [DisplayName("Tên người nhận")]
        public string Receiver { get; set; }

        [DisplayName("Số điện thoại người nhận")]
        public string PhoneReceiver { get; set; }


        [DisplayName("Ghi chú")]
        public string Notes { get; set; }

        [DisplayName("Ngày nhận hàng")]
        public DateTime ExpectedDate { get; set; }

        [DisplayName("Tổng tiền")]
        public decimal TotalMoney { get; set; }

        [DisplayName("Hình thức thanh toán")]
        public int TypePayment { get; set; }
        public string TypePayment_str { get; set; }

        [DisplayName("Địa chỉ nhận hàng")]
        public string Place { get; set; }

        public List<OrderDetailViewModel> lstDetail { get; set; }
        public List<SelectListItem> lstStatus { get; set; }
    }
}
