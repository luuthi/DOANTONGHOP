using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussiness.ViewModel;
using DataAccess;
using NSX_Common;

namespace Bussiness.Implement
{
    class ProductAttributeValueService : Interface.ProductAttributeValueService
    {
        public void DeleteProductAttributeValue(Guid Id)
        {
            SqlDb_Ultis.ExeNonStored("Product_Attribute_ValueDelete", "@Id", Id);
        }

        public List<ProductAttributeValueViewModel> GetAllProductAttributeValue()
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("Product_Attribute_ValueSelectAll");
            var lst = Ultis.DataTableToList<ProductAttributeValueViewModel>(dtb);
            return lst;
        }

        public ProductAttributeValueViewModel GetProductAttributeValueById(Guid Id)
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("Product_Attribute_ValueSelectByProId", "@Id", Id);
            ProductAttributeValueViewModel role = new ProductAttributeValueViewModel();
            foreach (DataRow item in dtb.Rows)
            {
                role = Ultis.GetItem<ProductAttributeValueViewModel>(item);
            }
            return role;
        }

        public List<ProductAttributeValueViewModel> GetProductAttributeValueByProId(Guid Id)
        {
            DataTable dtb = SqlDb_Ultis.ExeStoredToDataTable("Product_Attribute_ValueSelectByID", "@Id", Id);
            var lst = Ultis.DataTableToList<ProductAttributeValueViewModel>(dtb);
            return lst;
        }

        public void InsertProductAttributeValue(ProductAttributeValueViewModel viewModel)
        {
           SqlDb_Ultis.ExeNonStored("Product_Attribute_ValueInsert",
               "@Id", viewModel.Id,
               "@ProductId", viewModel.AttributeId,
               "@AttributeId", viewModel.ProductId,
               "@Value", viewModel.Value);
        }

        public void UpdateProductAttributeValue(ProductAttributeValueViewModel viewModel)
        {
            SqlDb_Ultis.ExeNonStored("Product_Attribute_ValueUpDate",
               "@Id", viewModel.Id,
               "@ProductId", viewModel.AttributeId,
               "@AttributeId", viewModel.ProductId,
               "@Value", viewModel.Value);
        }
    }
}
