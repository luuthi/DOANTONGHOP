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
    public class ProductGroupService : IProductGroupService
    {
        public void DeleteProductGroup(Guid Id)
        {
            SqlDb_Ultis.ExeNonStored("Tbl_ProductGroupDelete", "@Id", Id);
        }

        public List<ProductGroupViewModel> GetAllProductGroup()
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("Tbl_ProductGroupSelectAll");
            var lst = Ultis.DataTableToList<ProductGroupViewModel>(dtb);
            return lst;
        }

        public ProductGroupViewModel GetProductGroupById(Guid Id)
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("Tbl_ProductGroupSelectByID", "@Id", Id);
            ProductGroupViewModel role = new ProductGroupViewModel();
            foreach (DataRow item in dtb.Rows)
            {
                role = Ultis.GetItem<ProductGroupViewModel>(item);
            }
            return role;
        }

        public void InsertProductGroup(ProductGroupViewModel viewModel)
        {
            SqlDb_Ultis.ExeNonStored("Tbl_ProductGroupInsert",
                 "@Id", viewModel.Id,
                 "@GroupName", viewModel.GroupName,
                 "@Description", viewModel.Description,
                 "@Status", viewModel.Status,
                 "@Creator", viewModel.Creator,
                 "@CreatedDate", viewModel.CreatedDate,
                 "@Modifier", viewModel.Modifier,
                 "@ModifyDate", viewModel.ModifyDate,
                 "@Url",viewModel.Url);
        }

        public void UpdateProductGroup(ProductGroupViewModel viewModel)
        {
            SqlDb_Ultis.ExeNonStored("Tbl_ProductGroupUpDate",
                "@Id", viewModel.Id,
                "@GroupName", viewModel.GroupName,
                "@Description", viewModel.Description,
                "@Status", viewModel.Status,
                "@Modifier", viewModel.Modifier,
                "@ModifyDate", viewModel.ModifyDate,
                 "@Url", viewModel.Url);
        }
    }
}
