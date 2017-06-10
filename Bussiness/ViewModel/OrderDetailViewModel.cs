using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.ViewModel
{
    public class OrderDetailViewModel
    {
        [DisplayName("Mã đơn hàng")]
        public Guid  OrderId { get; set; }

        public string OrderCode { get; set; }
        [DisplayName("Sản phẩm")]
        public Guid ProductId { get; set; }

        public string ProductName { get; set; }
        public string Image { get; set; }

        [DisplayName("Số lượng")]
        public float Amount { get; set; }

        [DisplayName("Thành tiền")]
        public decimal Total { get; set; }

        [DisplayName("Đơn giá")]
        public decimal Price { get; set; }

        [DisplayName("Ghi chú")]
        public string Notes { get; set; }
    }
}
