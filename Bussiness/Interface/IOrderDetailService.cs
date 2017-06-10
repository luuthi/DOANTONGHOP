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
        OrderDetailViewModel GetOrderEtailById(Guid proid, Guid oid);
        void InsertOrderDetail(OrderDetailViewModel viewModel);
        void UpdateOrderDetail(OrderDetailViewModel viewModel);
        int GetOrderDetailByProduct(Guid id);
    }
}
