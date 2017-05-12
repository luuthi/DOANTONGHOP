using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussiness.ViewModel;

namespace Bussiness.Interface
{
    public interface IProductService
    {
        /// <summary>
        /// Lấy tất cả tài khoản
        /// </summary>
        /// <returns></returns>
        List<ProductViewModel> GetAllProduct();
        /// <summary>
        /// Lấy tài khoản theo id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        ProductViewModel GetProductById(Guid Id);
        /// <summary>
        /// thêm mới tài khoản
        /// </summary>
        /// <param name="viewModel"></param>
        void InsertProduct(ProductViewModel viewModel);
        /// <summary>
        /// cập nhật tài khoản
        /// </summary>
        /// <param name="viewModel"></param>
        void UpdateProduct(ProductViewModel viewModel);
        /// <summary>
        /// xóa tài khoản
        /// </summary>
        /// <param name="viewModel"></param>
        void DeleteProduct(Guid Id);
    }
}
