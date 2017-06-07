using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussiness.ViewModel;

namespace Bussiness.Interface
{
    public interface IProductAttributeValueService
    {
        /// <summary>
        /// Lấy tất cả tài khoản
        /// </summary>
        /// <returns></returns>
        List<ProductAttributeValueViewModel> GetAllProductAttributeValue();
        /// <summary>
        /// Lấy tài khoản theo id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        ProductAttributeValueViewModel GetProductAttributeValueById(Guid Id);
        /// <summary>
        /// Lấy tài khoản theo id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        List<ProductAttributeValueViewModel> GetProductAttributeValueByProId(Guid Id);
        /// <summary>
        /// thêm mới tài khoản
        /// </summary>
        /// <param name="viewModel"></param>
        void InsertProductAttributeValue(ProductAttributeValueViewModel viewModel);
        /// <summary>
        /// cập nhật tài khoản
        /// </summary>
        /// <param name="viewModel"></param>
        void UpdateProductAttributeValue(ProductAttributeValueViewModel viewModel);
        /// <summary>
        /// xóa tài khoản
        /// </summary>
        /// <param name="viewModel"></param>
        void DeleteProductAttributeValue(Guid Id);
    }
}
