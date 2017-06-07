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
    public class ProductAttributeService : IProductAttributeService
    {
        public void DeleteProductAttribute(Guid Id)
        {
            SqlDb_Ultis.ExeNonStored("Product_AttributeDelete", "@Id", Id);
        }

        public List<ProductAttributeViewModel> GetAllProducAttributet()
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("Product_AttributeSelectAll");
            var lst = Ultis.DataTableToList<ProductAttributeViewModel>(dtb);
            return lst;
        }

        public List<ProductAttributeViewModel> GetAllProducAttributebySP(Guid masanpham)
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("LoadAttr", "@Id", masanpham);
            var lst = Ultis.DataTableToList<ProductAttributeViewModel>(dtb);
            return lst;
        }
        public ProductAttributeViewModel GetProductAttributeById(Guid Id)
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("Product_AttributeSelectByID", "@Id", Id);
            ProductAttributeViewModel role = new ProductAttributeViewModel();
            foreach (DataRow item in dtb.Rows)
            {
                role = Ultis.GetItem<ProductAttributeViewModel>(item);
            }
            return role;
        }

        public void InsertProductAttribute(ProductAttributeViewModel viewModel)
        {
            SqlDb_Ultis.ExeNonStored("Product_AttributeInsert",
                "@Id", viewModel.Id,
                "@AttributeName", viewModel.AttributeName,
                "@AttributeType", viewModel.AttributeType);
        }

        public void UpdateProductAttribute(ProductAttributeViewModel viewModel)
        {
            SqlDb_Ultis.ExeNonStored("Product_AttributeUpDate",
                "@Id", viewModel.Id,
                "@AttributeName", viewModel.AttributeName,
                "@AttributeType", viewModel.AttributeType);
        }
    }
}
