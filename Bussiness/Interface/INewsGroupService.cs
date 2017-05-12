using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussiness.ViewModel;

namespace Bussiness.Interface
{
    public interface INewsGroupService
    {
        /// <summary>
        /// Lấy tất cả tài khoản
        /// </summary>
        /// <returns></returns>
        List<NewsGroupViewModel> GetAllNewsGroup();
        /// <summary>
        /// Lấy tài khoản theo id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        NewsGroupViewModel GetNewsGroupById(Guid Id);
        /// <summary>
        /// thêm mới tài khoản
        /// </summary>
        /// <param name="viewModel"></param>
        void InsertNewsGroup(NewsGroupViewModel viewModel);
        /// <summary>
        /// cập nhật tài khoản
        /// </summary>
        /// <param name="viewModel"></param>
        void UpdateNewsGroup(NewsGroupViewModel viewModel);
        /// <summary>
        /// xóa tài khoản
        /// </summary>
        /// <param name="viewModel"></param>
        void DeleteNewsGroup(Guid Id);
    }
}
