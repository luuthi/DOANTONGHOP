using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussiness.ViewModel;

namespace Bussiness.Interface
{
    public interface IOrderDetailService
    {
        List<OrderDetailViewModel> GetOrderDetailByOrder(Guid id);
        OrderDetailViewModel GetOrderEtailById(Guid id);
        void InsertOrderDetail(OrderDetailViewModel viewModel);
        void UpdateOrderDetail(OrderDetailViewModel viewModel);
    }
}
