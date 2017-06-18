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
    public class OrderService : IOrderService
    {
        public int CountOrderOnDay()
        {
            int count= SqlDb_Ultis.ExeStoredCount("Tbl_OrderSelectCountOnDay");
            return count;
        }

        public void DeleteOrder(Guid id)
        {
            SqlDb_Ultis.ExeNonStored("Tbl_OrderDelete","@Id",id);
        }

        public List<OrderViewModel> GetAllOrder()
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("Tbl_OrderSelectAll");
            var lst = Ultis.DataTableToList<OrderViewModel>(dtb);
            return lst;
        }

        public List<OrderViewModel> GetOrderByAccount(string id)
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("Tbl_OrderSelectByAccount","@Id",id);
            var lst = Ultis.DataTableToList<OrderViewModel>(dtb);
            return lst;
        }

        public OrderViewModel GetOrderById(Guid id)
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("Tbl_OrderSelectByID", "@Id", id);
            OrderViewModel role = new OrderViewModel();
            foreach (DataRow item in dtb.Rows)
            {
                role = Ultis.GetItem<OrderViewModel>(item);
            }
            return role;
        }

        public void InsertOrder(OrderViewModel viewModel)
        {
            SqlDb_Ultis.ExeNonStored("Tbl_OrderInsert",
                "@Id", viewModel.Id,
                "@OrderCode", viewModel.OrderCode,
                "@OrderDate", viewModel.OrderDate,
                "@OrderAccount", viewModel.OrderAccount,
                "@Status", viewModel.Status,
                "@Receiver", viewModel.Receiver,
                "@Place", viewModel.Place,
                "@PhoneReceiver", viewModel.PhoneReceiver,
                "@Notes", viewModel.Notes,
                "@ExpectedDate", viewModel.ExpectedDate,
                "@TotalMoney", viewModel.TotalMoney,
                "@TypePayment", viewModel.TypePayment);
        }

        public void UpdateOrder(OrderViewModel viewModel)
        {
            SqlDb_Ultis.ExeNonStored("Tbl_OrderUpDate",
                "@Id", viewModel.Id,
                "@Status", viewModel.Status);
        }
    }
}
