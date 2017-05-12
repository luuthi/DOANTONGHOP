using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussiness.ViewModel;

namespace Bussiness.Interface
{
    public interface IOrderService
    {
        List<OrderViewModel> GetAllOrder();
        OrderViewModel GetOrderById(Guid id);
        void InsertOrder(OrderViewModel viewModel);
        void UpdateOrder(OrderViewModel viewModel);
        void DeleteOrder(Guid id);
        List<OrderViewModel> GetOrderByAccount(Guid id);
    }
}
