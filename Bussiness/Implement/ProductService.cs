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
    public class ProductService : IProductService
    {
        public void DeleteProduct(Guid Id)
        {
            SqlDb_Ultis.ExeNonStored("Tbl_ProductDelete", "@Id", Id);
        }

        public List<ProductViewModel> GetAllProduct()
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("Tbl_ProductSelectAll");
            var lst = Ultis.DataTableToList<ProductViewModel>(dtb);
            return lst;
        }

        public ProductViewModel GetProductById(Guid Id)
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("Tbl_ProductSelectByID", "@Id", Id);
            ProductViewModel role = new ProductViewModel();
            foreach (DataRow item in dtb.Rows)
            {
                role = Ultis.GetItem<ProductViewModel>(item);
            }
            return role;
        }

        public void InsertProduct(ProductViewModel viewModel)
        {
            SqlDb_Ultis.ExeNonStored("Tbl_ProductInsert",
                "@Id", viewModel.Id,
                "@GroupId", viewModel.GroupId,
                "@ProductName", viewModel.ProductName,
                "@Price", viewModel.Price,
                "@Unit", viewModel.Unit,
                "@Image", viewModel.Image,
                "@Category", viewModel.Category,
                "@Status", viewModel.Status,
                "@CanComment", viewModel.CanComment,
                "@Description", viewModel.Description,
                "@DetailInfo", viewModel.DetailInfo,
                "@Vendor", viewModel.Vendor,
                "@Gallery", viewModel.Gallery,
                 "@Creator", viewModel.Creator,
                 "@CreatedDate", viewModel.CreatedDate,
                 "@Modifier", viewModel.Modifier,
                 "@ModifyDate", viewModel.ModifyDate);
        }

        public void UpdateProduct(ProductViewModel viewModel)
        {
            SqlDb_Ultis.ExeNonStored("Tbl_ProductUpDate",
                 "@Id", viewModel.Id,
                 "@GroupId", viewModel.GroupId,
                 "@ProductName", viewModel.ProductName,
                 "@Price", viewModel.Price,
                 "@Unit", viewModel.Unit,
                 "@Image", viewModel.Image,
                 "@Category", viewModel.Category,
                 "@Status", viewModel.Status,
                 "@CanComment", viewModel.CanComment,
                 "@Description", viewModel.Description,
                 "@DetailInfo", viewModel.DetailInfo,
                 "@Vendor", viewModel.Vendor,
                 "@Gallery", viewModel.Gallery,
                 "@Modifier", viewModel.Modifier,
                 "@ModifyDate", viewModel.ModifyDate);
        }
    }
}
