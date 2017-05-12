using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussiness.Interface;
using Bussiness.ViewModel;
using DataAccess;
using NSX_Common;

namespace Bussiness.Implement
{
    public class OrderDetailService : IOrderDetailService
    {
        public List<OrderDetailViewModel> GetOrderDetailByOrder(Guid id)
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("Tbl_OrderDetailSelectByOrder", "@Id", id);
            var lst = Ultis.DataTableToList<OrderDetailViewModel>(dtb);
            return lst;
        }

        public OrderDetailViewModel GetOrderEtailById(Guid id)
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("Tbl_OrderDetailSelectByID", "@Id", id);
            OrderDetailViewModel role = new OrderDetailViewModel();
            foreach (DataRow item in dtb.Rows)
            {
                role = Ultis.GetItem<OrderDetailViewModel>(item);
            }
            return role;
        }

        public void InsertOrderDetail(OrderDetailViewModel viewModel)
        {
            SqlDb_Ultis.ExeNonStored("Tbl_OrderDetailInsert",
                "@Id", viewModel.Id,
                "@OrderId", viewModel.OrderId,
                "@ProductId", viewModel.ProductId,
                "@Amount", viewModel.Amount,
                "@Total", viewModel.Total,
                "@Notes", viewModel.Notes);
        }

        public void UpdateOrderDetail(OrderDetailViewModel viewModel)
        {
            SqlDb_Ultis.ExeNonStored("Tbl_OrderDetailUpDate",
                "@Id", viewModel.Id,
                "@OrderId", viewModel.OrderId,
                "@ProductId", viewModel.ProductId,
                "@Amount", viewModel.Amount,
                "@Total", viewModel.Total,
                "@Notes", viewModel.Notes);
        }
    }
}
