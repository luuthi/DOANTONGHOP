﻿using System;
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

        public int GetOrderDetailByProduct(Guid id)
        {
           return  SqlDb_Ultis.ExeStoredGetInt("Tbl_OrderDetailSelectByProduct", "@Id", id);

        }
        public OrderDetailViewModel GetOrderEtailById(Guid proid,Guid oid)
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("Tbl_OrderDetailSelectByID", "@ProductId", proid,"@OrderID",oid);
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
                "@OrderId", viewModel.OrderId,
                "@ProductId", viewModel.ProductId,
                "@Amount", viewModel.Amount,
                "@Notes", viewModel.Notes,
                "@Price",viewModel.Price);
        }

        public void UpdateOrderDetail(OrderDetailViewModel viewModel)
        {
            SqlDb_Ultis.ExeNonStored("Tbl_OrderDetailUpDate",
                "@OrderId", viewModel.OrderId,
                "@ProductId", viewModel.ProductId,
                "@Amount", viewModel.Amount,
                "@Notes", viewModel.Notes,
                "@Price", viewModel.Price);
        }
    }
}
