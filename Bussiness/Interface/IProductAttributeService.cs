using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussiness.ViewModel;

namespace Bussiness.Interface
{
    public interface IProductAttributeService
    {
        /// <summary>
        /// Lấy tất cả tài khoản
        /// </summary>
        /// <returns></returns>
        List<ProductAttributeViewModel> GetAllProducAttributet();
        /// <summary>
        /// Lấy tất cả tài khoản
        /// </summary>
        /// <returns></returns>
        List<ProductAttributeViewModel> GetAllProducAttributebySP(Guid masanpham);
        /// <summary>
        /// Lấy tài khoản theo id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        ProductAttributeViewModel GetProductAttributeById(Guid Id);
        /// <summary>
        /// thêm mới tài khoản
        /// </summary>
        /// <param name="viewModel"></param>
        void InsertProductAttribute(ProductAttributeViewModel viewModel);
        /// <summary>
        /// cập nhật tài khoản
        /// </summary>
        /// <param name="viewModel"></param>
        void UpdateProductAttribute(ProductAttributeViewModel viewModel);
        /// <summary>
        /// xóa tài khoản
        /// </summary>
        /// <param name="viewModel"></param>
        void DeleteProductAttribute(Guid Id);
    }
}
